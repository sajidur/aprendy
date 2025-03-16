using Apprendi.Application.Common.Behaviours;
using Apprendi.Application.Factories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Apprendi.Application.Common.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationCommon(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjectionExtensions).Assembly);

            services.AddMediatR(configuration =>
            {
                var assembly = typeof(GeneratedPipelineBehaviorRegistrationExtensions).Assembly;
                configuration.RegisterServicesFromAssembly(assembly);
            });

            services.AddGeneratedPipelineBehaviors();
            services.AddScoped(typeof(IValidationContextFactory), typeof(ValidationContextFactory));

            services.AddHttpClient();
            services.AddOptions();

            services
                .AddOptions<ApiRequestHandlerOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(nameof(ApiRequestHandlerOptions)).Bind(settings);
                });

            return services;
        }
    }
}
