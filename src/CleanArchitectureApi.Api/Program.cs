using CleanArchitectureApi.Api.Extensions;
using CleanArchitectureApi.Application;
using CleanArchitectureApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();