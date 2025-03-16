using Apprendi.Web.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Apprendi.Web.Client.Components
{
    public partial class LocalTimeZoneComponent : ComponentBase, IAsyncDisposable
    {
        public readonly record struct TimeZoneInfo(string TimeZone, string Locale, int TimeZoneOffset);

        [Inject] 
        private ILocalTimeZoneSetterService TimeZoneService { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        private IJSObjectReference _module;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var modulePath = $"/Components/{nameof(LocalTimeZoneComponent)}.razor.js";
                _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", modulePath);
                var info = await _module.InvokeAsync<TimeZoneInfo>("getTimeZoneInfo");
                TimeZoneService.SetZoneInfo(info.Locale, info.TimeZone, info.TimeZoneOffset);
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (_module is not null)
            {
                try
                {
                    await _module.DisposeAsync();
                }
                catch (JSDisconnectedException)
                {
                    //do nothing
                }
            }
        }
    }
}
