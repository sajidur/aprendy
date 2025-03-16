using Apprendi.Application.Features.ReferenceData;
using Apprendi.Application.Features.SignUp.Commands.SignUpAsTutor;
using Apprendi.Web.Client.Services;
using Apprendi.Web.Client.Services.ApiRequestClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Apprendi.Web.Client.Pages.LogIn
{
    public sealed partial class TutorSignUp : ComponentBase
    {
        [Inject] private IApiRequestClient ApiRequestClient { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private ILocalTimeZoneService TimeZoneService { get; set; }

        private bool _isBusy;
        private string IsBusyClass => _isBusy ? "disabled" : "";

        [CascadingParameter]
        public LanguageDto SelectedLanguage { get; set; }

        [CascadingParameter]
        public LanguageDto SelectedCurrency { get; set; }

        private SignUpAsTutorCommand _signUpAsTutorCommand = new SignUpAsTutorCommand();

        private async Task OnSignUpClick()
        {
            _isBusy = true;

            var info = await TimeZoneService.GetTimeZone();

            _signUpAsTutorCommand.CurrencyId = SelectedCurrency.Id;
            _signUpAsTutorCommand.LanguageId = SelectedLanguage.Id;
            _signUpAsTutorCommand.TimeZoneId = info.TimeZoneId;

            var response = await ApiRequestClient.Send(_signUpAsTutorCommand);

            if (response.IsSuccess)
            {
                await LoginAsync();
                
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

        private async Task LoginAsync()
        {
            _isBusy = true; 

            var request = new LoginRequest
            {
                Username = _signUpAsTutorCommand.Email,
                Password = _signUpAsTutorCommand.Password
            };

            var response = await ApiRequestClient.LoginAsync(request);

            if (response.IsSuccess)
            {
                NavigationManager.NavigateTo("/", forceLoad: true);                
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
