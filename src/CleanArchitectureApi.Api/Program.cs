using CleanArchitectureApi.Api.Extensions;
using CleanArchitectureApi.Api.Filters;
using CleanArchitectureApi.Application;
using CleanArchitectureApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new ValidationFilterAttribute());
});

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

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