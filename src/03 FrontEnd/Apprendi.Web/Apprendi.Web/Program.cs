using Apprendi.Application.Factories;
using Apprendi.Web.Client.Extensions;
using Apprendi.Web.Client.Services.ApiRequestClient;
using Apprendi.Web.Components;
using Apprendi.Web.Services;
using Azure.Identity;
using BitzArt.Blazor.Auth.Server;
using BlazorPro.BlazorSize;
using FluentValidation;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Radzen;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddAuthorization();

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddWebClientServices();
builder.Services.AddRadzenComponents();
builder.Services.AddControllers();

builder.Services.AddResizeListener();
builder.Services.AddMediaQueryService();

builder.Services.AddSignalR(configure =>
{
    configure.EnableDetailedErrors = true;
    configure.MaximumReceiveMessageSize = 96 * 1024;
});

builder.Services.AddSingleton<IPublicClientApplication>((serviceProvier) =>
{
    var configuration = serviceProvier.GetService<IConfiguration>();
    var authority = configuration["AzureAd:Authority"];
    var tenantId = configuration["AzureAd:TenantId"];
    var clientId = configuration["AzureAd:ClientId"];


    var client = PublicClientApplicationBuilder
                    .Create(clientId)
                    .WithAuthority(authority)
                    .WithTenantId(tenantId)
                    .Build();
    return client;
});

builder.Services.AddScoped<GraphServiceClient>((serviceProvider) =>
{
    var configuration = serviceProvider.GetService<IConfiguration>();
    var authority = configuration["AzureAd:Authority"];
    var clientId = configuration["AzureAd:ClientId"];
    var tenantId = configuration["AzureAd:TenantId"];
    var clientSecret = configuration["AzureAd:ClientCredentials:0:ClientSecret"];
    var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
    return new GraphServiceClient(credential);
});

builder.Services.AddScoped<IJwtService, JwtService>();
builder.AddBlazorAuth<ApprendiAuthenticationService>();
builder.Services.AddOptions<JwtServiceOptions>()
                .Configure<IConfiguration>((configure, configuration) =>
                {
                    configuration.GetSection(nameof(JwtServiceOptions)).Bind(configure);
                });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

//app.UseMiddleware<EnsureUserExistsMiddleware>();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Apprendi.Web.Client._Imports).Assembly);

app.MapControllers();
app.MapAuthEndpoints();

app.Run();
