using Microsoft.Extensions.Options;
using Quartz;

namespace CleanArchitectureApi.Infrastructure.Outbox;

public class ProcessOutboxMessagesJobsSetup(IOptions<OutboxOptions> outboxOptions) : IConfigureOptions<QuartzOptions>
{
    private readonly OutboxOptions _outboxOptions = outboxOptions.Value;

    public void Configure(QuartzOptions options)
    {
        const string jobName = nameof(ProcessOutboxMessagesJobs);

        options
            .AddJob<ProcessOutboxMessagesJobs>(c => c.WithIdentity(jobName))
            .AddTrigger(c =>
                c.ForJob(jobName).WithSimpleSchedule(s =>
                    s.WithIntervalInSeconds(_outboxOptions.IntervalInSeconds).RepeatForever()
                )
            );
    }
}