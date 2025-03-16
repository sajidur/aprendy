using Apprendi.Application.Features.ReferenceData;
using Apprendi.Application.Features.ReferenceData.Queries.GetTimeZones;
using Apprendi.Application.Features.Users;
using Apprendi.Web.Client.Services;
using Apprendi.Web.Client.Services.ApiRequestClient;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Apprendi.Web.Client.Pages.Account
{
    public sealed partial class Account : ComponentBase, IDisposable
    {
        [Inject] private PersistentComponentState ApplicationState { get; set; }
        [Inject] private IApiRequestClient ApiRequestClient { get; set; }
        [Inject] private IDialogService DialogService { get; set; }

        [CascadingParameter]
        public UserDto CurrentUser { get; set; }

        private PersistingComponentStateSubscription _persistingSubscription;
        private string _selectedTimeZoneId;
        private List<TimeZoneDto> _timeZones = new();
        private IEnumerable<TimeZoneDto> _filteredTimeZones = Array.Empty<TimeZoneDto>();
        private string _initializationErrorMessage;

        protected override async Task OnInitializedAsync()
        {
            _persistingSubscription = ApplicationState.RegisterOnPersisting(PersistState);

            if (ApplicationState.TryTakeFromJson<AccountComponentState>(nameof(AccountComponentState), out var state))
            {
                _timeZones = state.TimeZones;
                _filteredTimeZones = state.TimeZones;
                _selectedTimeZoneId = state.SelectedTimeZoneId;
                _initializationErrorMessage = state.InitializationErrorMessage;
            }
            else
            {
                var response = await ApiRequestClient.Send(new GetTimeZonesQuery());

                if (response.IsSuccess)
                {
                    _timeZones = response.TimeZones;
                    _filteredTimeZones = _timeZones;

                    if (CurrentUser != null)
                    {
                        _selectedTimeZoneId = CurrentUser.TimeZoneId;
                    }
                }
                else
                {
                    _initializationErrorMessage = response.ErrorMessage;
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (!string.IsNullOrWhiteSpace(_initializationErrorMessage))
                {
                    await DialogService.DisplayErrorMessageWithRefreshAsync(_initializationErrorMessage);
                }
            }
        }

        void LoadData(LoadDataArgs args)
        {
            if (!string.IsNullOrEmpty(args.Filter))
            {
                _filteredTimeZones = _timeZones
                    .Where(x => $"{FormatTimeZoneName(x.Name)} ({x.Abbreviation} {FormatTimeZoneOffset(x.Offset)})".Contains(args.Filter, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                _filteredTimeZones = _timeZones;
            }
            InvokeAsync(StateHasChanged);
        }

        private string FormatTimeZoneOffset(double offsetInHours)
        {
            var timeSpan = TimeSpan.FromHours(offsetInHours);                    
            
            return timeSpan < TimeSpan.Zero
                ? $"-{timeSpan:hh\\:mm}"
                : $"{timeSpan:hh\\:mm}";
        }

        private string FormatTimeZoneName(string timeZoneName)
        {
            return timeZoneName.Replace('_', ' ');
        }

        private Task PersistState()
        {
            var state = new AccountComponentState
            {
                TimeZones = _timeZones,
                SelectedTimeZoneId = _selectedTimeZoneId,
                InitializationErrorMessage = _initializationErrorMessage
            };

            ApplicationState.PersistAsJson(nameof(AccountComponentState), state);
            return Task.CompletedTask;
        }

        void IDisposable.Dispose()
        {
            _persistingSubscription.Dispose();            
        }
    }

    public class AccountComponentState
    {
        public List<TimeZoneDto> TimeZones { get; set; }
        public string SelectedTimeZoneId { get; set; }
        public string InitializationErrorMessage { get; set; }
    }
}
