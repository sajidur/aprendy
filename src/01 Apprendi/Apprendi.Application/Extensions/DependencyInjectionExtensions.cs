using Apprendi.Application.Common.Behaviours;
using Apprendi.Application.Common.Services;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.SignUp;
using Apprendi.Application.Features.Tutors;
using Apprendi.Application.Features.Users;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph.Models.ExternalConnectors;

namespace Apprendi.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjectionExtensions).Assembly);

            services.AddMediatR(configuration =>
            {
                var assembly = typeof(DependencyInjectionExtensions).Assembly;
                configuration.RegisterServicesFromAssembly(assembly);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ServerSideOnlyValidationBehaviour<,>));

            services.AddSingleton(typeof(IResponseFactory<>), typeof(ResponseFactory<>));
            services.AddTransient<ITimerFactory, TimerFactory>();
            services.AddScoped<IValidationContextFactory, ValidationContextFactory>();

            services.AddScoped<IUserMapper, UserMapper>();
            services.AddScoped<ISignupMapper, SignupMapper>();
            services.AddScoped<ITutorMapper, TutorMapper>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            
            services.AddOptions<SignupMapperOptions>()
                .Configure<IConfiguration>((configure, configuration) =>
                {
                    configuration.GetSection(nameof(SignupMapperOptions)).Bind(configure);
                });

            return services;
        }
    }
}
