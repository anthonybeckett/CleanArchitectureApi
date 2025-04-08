using CleanArchitectureApi.Domain.Abstractions;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace CleanArchitectureApi.Infrastructure.Outbox;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJobs(
    IPublisher publisher,
    ILogger<ProcessOutboxMessagesJobs> logger,
    OutboxOptions outboxOptions,
    ApplicationDbContext dbContext
) : IJob
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Execute outbox messages job");

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            var outboxMessages = await GetOutboxMessagesAsync(dbContext);

            foreach (var outboxMessage in outboxMessages)
            {
                Exception? exception = null;

                try
                {
                    var domainEvent =
                        JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content, JsonSerializerSettings);

                    if (domainEvent == null) throw new ArgumentNullException(nameof(domainEvent));

                    await publisher.Publish(domainEvent, context.CancellationToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message, outboxMessage.Id);

                    exception = ex;
                }

                await UpdateOutboxMessagesAsync(dbContext, outboxMessage, exception);
            }

            await dbContext.SaveChangesAsync(context.CancellationToken);

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            logger.LogError(ex, ex.Message);

            throw new Exception();
        }
    }

    private async Task<IReadOnlyList<OutboxMessagesResponse>> GetOutboxMessagesAsync(ApplicationDbContext dbContext)
    {
        var outboxMessages = await dbContext
            .OutboxMessages
            .FromSqlRaw(
                """
                    SELECT TOP (@BatchSize) Id, Content
                    FROM OutboxMessages WITH (UPDLOCK)
                    WHERE ProcessedOnUtc IS NULL OR Error IS NOT NULL
                    ORDER BY OccuredOnUtc
                """
                , new SqlParameter("@BatchSize", outboxOptions.BatchSize)
            )
            .Select(x => new OutboxMessagesResponse(x.Id, x.Content))
            .ToListAsync();

        return outboxMessages;
    }

    private async Task UpdateOutboxMessagesAsync(ApplicationDbContext dbContext,
        OutboxMessagesResponse outboxMessageResponse,
        Exception? exception)
    {
        var outboxMessage = await dbContext.OutboxMessages.FindAsync(outboxMessageResponse.Id);

        outboxMessage!.Update(DateTime.UtcNow, exception?.ToString()!);
    }

    internal sealed record OutboxMessagesResponse(Guid Id, string Content);
}