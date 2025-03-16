using Apprendi.Application.Features.ReferenceData;
using Apprendi.Application.Features.ReferenceData.Queries.GetReferenceData;
using Apprendi.Application.Features.Users;
using Apprendi.Application.Features.Users.Queries.GetUserByEmail;
using Apprendi.Domain.Enums;
using Apprendi.Web.Client.Extensions;
using Apprendi.Web.Client.Pages.SignUp;
using Apprendi.Web.Client.Services;
using Apprendi.Web.Client.Services.ApiRequestClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Radzen.Blazor;
using Radzen.Blazor.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Apprendi.Web.Client.Layout
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        [Inject] private PersistentComponentState ApplicationState { get; set; }
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IApiRequestClient ApiRequestClient { get; set; }
        [Inject] private IDialogService DialogService { get; set; }

        private bool _hasRendered;
        private Popup _popupLanguageCurrency;
        private Popup _popupProfileMenu;
        private RadzenButton _languageCurrencyButton;
        private RadzenButton _profileMenuButton;
        private List<RoleDto> _roles = new();
        private List<LanguageDto> _languages = new();
        private List<CurrencyDto> _currencies = new();
        private UserDto CurrentUser;
        private LanguageDto SelectedLanguage = new();
        private CurrencyDto SelectedCurrency = new();
        private bool _isInitialized;
        private string _initializationErrorMessage;


        private PersistingComponentStateSubscription _persistingSubscription;

        protected override async Task OnInitializedAsync()
        {
            _persistingSubscription = ApplicationState.RegisterOnPersisting(PersistState);

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (ApplicationState.TryTakeFromJson<MainLayoutComponentState>(nameof(MainLayoutComponentState), out var state))
            {
                _currencies = state.Currencies;
                _languages = state.Languages;
                _roles = state.Roles;
                CurrentUser = state.CurrentUser;
                SelectedCurrency = state.SelectedCurrency ?? state.Currencies.First();
                SelectedLanguage = state.SelectedLanguage ?? state.Languages.First();
                _initializationErrorMessage = state.InitializationErrorMessage;
            }
            else
            {
                var referenceDataResponseTask = ApiRequestClient.Send(new GetReferenceDataQuery());

                Task<GetUserByEmailQueryResponse> userResponseTask = null;

                if (authState.User?.Identity?.IsAuthenticated == true)
                {
                    userResponseTask = ApiRequestClient.Send(new GetUserByEmailQuery()
                    {
                        Email = authState.User.Identity.Name
                    });
                }
                
                await Task.WhenAll(referenceDataResponseTask, userResponseTask ?? Task.CompletedTask);

                var referenceDataResponse = await referenceDataResponseTask;                

                if (referenceDataResponse.IsSuccess)
                {                    
                    _currencies = referenceDataResponse.Currencies;
                    _languages = referenceDataResponse.Languages;
                    _roles = referenceDataResponse.Roles;

                    SelectedLanguage = _languages.First();
                    SelectedCurrency = _currencies.First();
                }
                else
                {
                    _initializationErrorMessage = referenceDataResponse.ErrorMessage;
                    return;
                }

                if (userResponseTask != null)
                {
                    var userResponse = await userResponseTask;

                    if (userResponse.IsSuccess)
                    {
                        CurrentUser = userResponse.User;
                        RedirectIfNeeded(userResponse);
                    }
                    else
                    {
                        _initializationErrorMessage = referenceDataResponse.ErrorMessage;
                    }
                }
            }
        }

        private void RedirectIfNeeded(GetUserByEmailQueryResponse response)
        {
            var user = response.User;            
            if (user.IsTutor())
            {
                var path = new Uri(NavigationManager.Uri).AbsolutePath.ToLower();

                var personalInformationPath = "/signup/tutor/personal-information";
                var teachingProfilePath = "/signup/tutor/teaching-profile"; 

                var isPersonalInformationPath = path.Equals(personalInformationPath, StringComparison.OrdinalIgnoreCase);
                var isTeachingProfilePath = path.Equals(teachingProfilePath, StringComparison.OrdinalIgnoreCase);

                if (!isPersonalInformationPath && !isTeachingProfilePath)
                {
                    var signUpStage = response.TutorInformation.TutorSignUpStage;

                    switch (signUpStage)
                    {
                        case TutorSignUpStage.PersonalInformation:
                            NavigationManager.NavigateTo(personalInformationPath);
                            break;
                        case TutorSignUpStage.TeachingProfile:
                            NavigationManager.NavigateTo(teachingProfilePath);
                            break;
                    }
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {            
            if (firstRender)
            {
                _isInitialized = true;
                if (!string.IsNullOrWhiteSpace(_initializationErrorMessage))
                {
                    await DialogService.DisplayErrorMessageWithRefreshAsync(_initializationErrorMessage);
                }
            }
        }

        private Task PersistState()
        {
            var state = new MainLayoutComponentState
            {
                Currencies = _currencies,
                Languages = _languages,
                Roles = _roles,
                CurrentUser = CurrentUser,
                SelectedCurrency = SelectedCurrency,
                SelectedLanguage = SelectedLanguage,
                InitializationErrorMessage = _initializationErrorMessage
            };

            ApplicationState.PersistAsJson(nameof(MainLayoutComponentState), state);
            return Task.CompletedTask;
        }
        
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _hasRendered = true;
                StateHasChanged();
            }
        }

        private async Task OnLanguageCurrencyClick()
        {
            await _popupLanguageCurrency.ToggleAsync(_languageCurrencyButton.Element);
        }

        private async Task OnProfileMenuClick()
        {
            await _popupProfileMenu.ToggleAsync(_profileMenuButton.Element);
        }

        private void OnLoginClick()
        {   
            var url = new Uri(NavigationManager.Uri).AbsolutePath == "/login"
                ? "/login"
                : $"/login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}";

            NavigationManager.NavigateToLogin(url);
        }

        private async Task OnLogoutClick()
        {
            var response = await ApiRequestClient.Logout();

            if (!response.IsSuccess)
            {
                await DialogService.DisplayError(response.ErrorMessage);
            }
            else
            {
                NavigationManager.NavigateTo("/", forceLoad: true);
            }   
        }

        private async Task OnSettingsClick()
        {
            NavigationManager.NavigateTo("settings/account", forceLoad: false);
            await _popupProfileMenu.CloseAsync();
        }

        void IDisposable.Dispose()
        {
            _persistingSubscription.Dispose();
        }
    }

    public class MainLayoutComponentState
    {
        public List<RoleDto> Roles { get; set; } = new();
        public List<LanguageDto> Languages { get; set; } = new();
        public List<CurrencyDto> Currencies { get; set; } = new();
        public LanguageDto SelectedLanguage { get; set; }
        public CurrencyDto SelectedCurrency { get; set; }
        public UserDto CurrentUser { get; set; }
        public string InitializationErrorMessage { get; set; }
    }
}
