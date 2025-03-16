using Apprendi.Application.Features.ReferenceData;
using Apprendi.Application.Features.SignUp.Commands.SignUpAsStudent;
using Apprendi.Web.Client.Services;
using Apprendi.Web.Client.Services.ApiRequestClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Apprendi.Web.Client.Pages.LogIn
{
    public sealed partial class StudentSignUp : ComponentBase
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

        private SignUpAsStudentCommand _signUpAsStudentCommand = new SignUpAsStudentCommand();

        private async Task OnSignUpClick()
        {
            _isBusy = true;

            var info = await TimeZoneService.GetTimeZone();

            _signUpAsStudentCommand.CurrencyId = SelectedCurrency.Id;
            _signUpAsStudentCommand.LanguageId = SelectedLanguage.Id;
            _signUpAsStudentCommand.TimeZoneId = info.TimeZoneId;

            var response = await ApiRequestClient.Send(_signUpAsStudentCommand);

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
                Username = _signUpAsStudentCommand.Email,
                Password = _signUpAsStudentCommand.Password
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
