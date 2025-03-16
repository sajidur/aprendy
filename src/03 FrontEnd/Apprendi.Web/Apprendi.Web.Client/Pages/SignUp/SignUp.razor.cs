using Apprendi.Application.Features.ReferenceData;
using Apprendi.Application.Features.SignUp.Commands.SignUpAsStudent;
using Apprendi.Application.Features.SignUp.Commands.SignUpAsTutor;
using Apprendi.Application.Features.SignUp.Queries;
using Apprendi.Application.Features.Users;
using Apprendi.Web.Client.Extensions;
using Apprendi.Web.Client.Services;
using Apprendi.Web.Client.Services.ApiRequestClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Apprendi.Web.Client.Pages.SignUp
{
    public partial class SignUp : ComponentBase
    {
        [Inject] 
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private IApiRequestClient ApiRequestClient { get; set; }

        [Inject]
        private ILocalTimeZoneService TimeZoneService { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public UserDto CurrentUser { get; set; }

        [CascadingParameter]
        public LanguageDto SelectedLanguage { get; set; }

        [CascadingParameter]
        public LanguageDto SelectedCurrency { get; set; }

        protected override async Task OnInitializedAsync()
        { 
            if (CurrentUser == null) return;

            if (CurrentUser.IsTutor() == true)
            {
                var request = new GetTutorSignUpStageQuery
                {
                    TutorId = CurrentUser.Id,
                };

                var response = await ApiRequestClient.Send(request);
                
                if (response.IsSuccess)
                {
                    NavigationManager.NavigateTo($"/signup/tutor/{response.SignUpStage}");
                }
                else
                {
                    //todo:
                }
            }
            else if (CurrentUser.IsStudent() == true)
            {
                NavigationManager.NavigateTo($"/");
            }
        }

        private async Task OnChooseStudentUpClick()
        {
            var info = await TimeZoneService.GetTimeZone();
            var request = new SignUpAsStudentCommand
            {
                CurrencyId = SelectedCurrency.Id,
                LanguageId = SelectedLanguage.Id,
                TimeZoneId = info.TimeZoneId,
                //UserId = CurrentUser.Id
            };

            var response = await ApiRequestClient.Send(request);

            if (response.IsSuccess)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                await DialogService.DisplayError(response.ErrorMessage);
            }
        }

        private async Task OnChooseTutorUpClick()
        {
            var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (!state.User.Identity.IsAuthenticated)
            {
                var message = "Session expired, please refresh the page";
                await DialogService.DisplayError(message);
            }
            else
            {
                if (state.User.Identity is ClaimsIdentity claimsIdentity)
                {
                    var info = await TimeZoneService.GetTimeZone();

                    var name = claimsIdentity.FindFirst("name").Value;
                    var email = claimsIdentity.Name;

                    var request = new SignUpAsTutorCommand
                    {
                        //FirstName = name,
                        Email = email,
                        CurrencyId = SelectedCurrency.Id,
                        LanguageId = SelectedLanguage.Id,
                        TimeZoneId = info.TimeZoneId,                        
                    };

                    var response = await ApiRequestClient.Send(request);

                    if (response.IsSuccess)
                    {
                        NavigationManager.NavigateTo("/signup/tutor/about");
                    }
                    else
                    {
                        await DialogService.DisplayError(response.ErrorMessage);
                    }
                }
                else
                {
                    //todo:
                }
            }
        }
    }
}
