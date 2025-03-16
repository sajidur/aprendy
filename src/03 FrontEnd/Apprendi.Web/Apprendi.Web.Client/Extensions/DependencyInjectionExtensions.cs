using Apprendi.Application.Common.Extensions;
using Apprendi.Application.Factories;
using Apprendi.Web.Client.Services;
using Apprendi.Web.Client.Services.ApiRequestClient;
using FluentValidation;
using Radzen;

namespace Apprendi.Web.Client.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddWebClientServices(this IServiceCollection services)
        {
            services.AddApplicationCommon();
            services.AddScoped<IApiRequestClient, ApiRequestClient>();
            
            services.AddScoped<ILocalTimeZoneSetterService, LocalTimeZoneService>();
            services.AddScoped<ILocalTimeZoneService>(serviceProvider =>
            {
                return (LocalTimeZoneService)serviceProvider.GetRequiredService<ILocalTimeZoneSetterService>();
            });

            services.AddSingleton<IResponseFactory<LoginResponse>, ResponseFactory<LoginResponse>>();
            services.AddSingleton<IResponseFactory<LogoutResponse>, ResponseFactory<LogoutResponse>>();
            services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();

            services.AddScoped<IDialogService, Services.DialogService>();

            services.AddRadzenComponents();

            return services;
        }
    }
}
