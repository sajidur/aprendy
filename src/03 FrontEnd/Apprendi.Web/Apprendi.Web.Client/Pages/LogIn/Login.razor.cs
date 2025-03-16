using Apprendi.Application.Features.ReferenceData;
using Apprendi.Web.Client.Services;
using Apprendi.Web.Client.Services.ApiRequestClient;
using Microsoft.AspNetCore.Components;

namespace Apprendi.Web.Client.Pages.LogIn
{
    public sealed partial class Login : ComponentBase
    {
        [Inject] private IApiRequestClient ApiRequestClient { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IDialogService DialogService { get; set; }

        private bool _isBusy;
        private string IsBusyClass => _isBusy ? "disabled" : "";

        [CascadingParameter]
        public LanguageDto SelectedLanguage { get; set; }

        [CascadingParameter]
        public LanguageDto SelectedCurrency { get; set; }

        private LoginRequest _loginRequest = new LoginRequest();

        [Parameter]
        [SupplyParameterFromQuery(Name = nameof(ReturnUrl))]
        public string ReturnUrl { get; set; }

        private async Task OnLoginClick()
        {
            _isBusy = true;

            var response = await ApiRequestClient.LoginAsync(_loginRequest);
            if (response.IsSuccess)
            {
                NavigationManager.NavigateTo(ReturnUrl ?? "/", forceLoad: true);                
            }
            else if (!string.IsNullOrWhiteSpace(response.ErrorMessage))
            {
                await DialogService.DisplayError(response.ErrorMessage);
                _isBusy = false;
            }
            else
            {
                _isBusy = false;
            }   
        }
    }
}
