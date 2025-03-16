using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Features.SignUp.Commands.RegisterUserAsStudent;
using Apprendi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Microsoft.Graph;
using Apprendi.Application.Features.SignUp;

namespace Apprendi.Infrastructure.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContextFactory<ApprendiDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString, b =>
                {
                    b.MigrationsAssembly(typeof(ApprendiDbContext).Assembly.FullName);
                });
            });

            services.AddSingleton<IDateTimeOffset, DateTimeOffsetWrapper>();
            services.AddScoped<IApprendiDbContext, ApprendiDbContext>();

            services.AddScoped<IConfidentialClientApplication>((serviceProvider) =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var authority = configuration["AzureAd:Authority"];
                var clientId = configuration["AzureAd:ClientId"];
                var clientSecret = configuration["AzureAd:ClientCredentials:ClientSecret:0:ClientSecret"];

                return ConfidentialClientApplicationBuilder.Create(clientId)
                            .WithClientSecret(clientSecret)
                            .WithAuthority(authority)
                            .Build();
            });

            services.AddScoped<GraphServiceClient>((serviceProvider) =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var authority = configuration["AzureAd:Authority"];
                var clientId = configuration["AzureAd:ClientId"];
                var tenantId = configuration["AzureAd:TenantId"];
                var clientSecret = configuration["AzureAd:ClientCredentials:ClientSecret:0:ClientSecret"];

                var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                return new GraphServiceClient(credential);
            });

            return services;
        }
    }
}
