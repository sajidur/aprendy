using Apprendi.Web.Client.Extensions;
using BitzArt.Blazor.Auth.Client;
using BlazorPro.BlazorSize;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

builder.Services.AddWebClientServices();
builder.Services.AddRadzenComponents();

builder.Services.AddResizeListener();
builder.Services.AddMediaQueryService();

builder.AddBlazorAuth();

await builder.Build().RunAsync();
