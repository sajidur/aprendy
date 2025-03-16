using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Extensions;
using Apprendi.Domain.Entities;
using Apprendi.Domain.Enums;
using Apprendi.Infrastructure.Extensions;
using Apprendi.Infrastructure.Persistence;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplication()
    .AddInfrastructure();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();


var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetService<IApprendiDbContext>() as ApprendiDbContext;
    context.Database.Migrate();
}

app.Run();
