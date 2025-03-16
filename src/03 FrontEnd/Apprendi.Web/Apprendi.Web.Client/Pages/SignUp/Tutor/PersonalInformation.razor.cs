using Apprendi.Application.Features.ReferenceData;
using Apprendi.Application.Features.Tutors;
using Apprendi.Application.Features.Tutors.Command.UpdateTutorPersonalInformation;
using Apprendi.Application.Features.Tutors.Queries.GetTutorPersonalInformation;
using Apprendi.Application.Features.Users;
using Apprendi.Web.Client.Services;
using Apprendi.Web.Client.Services.ApiRequestClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;

namespace Apprendi.Web.Client.Pages.SignUp.Tutor
{
    public partial class PersonalInformation : ComponentBase, IDisposable
    {
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] private PersistentComponentState ApplicationState { get; set; }
        [Inject] private IApiRequestClient ApiRequestClient { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private PersistingComponentStateSubscription _persistingSubscription;

        private readonly UpdateTutorPersonalInformationCommand _command = new();
        private string _initializationErrorMessage;
        private List<LanguageProficiencyLevelDto> _languageProficiencyLevels;
        private List<LanguageDto> _languages;
        private List<CountryDto> _countries;

        [CascadingParameter]
        public UserDto CurrentUser { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _persistingSubscription = ApplicationState.RegisterOnPersisting(PersistState);

            if (ApplicationState.TryTakeFromJson<PersonalInformationComponentState>(nameof(PersonalInformationComponentState), out var state))
            {
                _command.PersonalInformation = state.TutorPersonalInformation;
                _languageProficiencyLevels = state.LanguageProficiencyLevels;
                _languages = state.Languages;
                _countries = state.Countries;
                _initializationErrorMessage = state.InitializationErrorMessage;
            }
            else
            {
                var email = await GetEmail();

                var response = await ApiRequestClient.Send(new GetTutorPersonalInformationQuery { Email = email });

                if (response.IsSuccess)
                {
                    _command.PersonalInformation = response.TutorPersonalInformation;                    
                    _languageProficiencyLevels = response.LanguageProficiencyLevels;
                    _languages = response.Languages;
                    _countries = response.Countries;
                    EnsureThereIsAtLeastOneSpokenLanguage();
                }
                else
                {
                    _initializationErrorMessage = response.ErrorMessage;
                }
            }
        }

        private async Task<string> GetEmail()
        {
            if (CurrentUser != null)
            {
                return CurrentUser.Email;
            }
            else
            {
                var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                return authenticationState.User.Identity.Name;
            }
        }

        private void EnsureThereIsAtLeastOneSpokenLanguage()
        {
            if (_command.PersonalInformation.SpokenLanguages.Count == 0)
            {
                _command.PersonalInformation.SpokenLanguages.Add(new SpokenLanguageDto());
            }
        }

        private async Task OnContinueClick()
        {
            var response = await ApiRequestClient.Send(_command);

            if (response.IsSuccess)
            {
                NavigationManager.NavigateTo("/signup/tutor/teaching-profile");
            }
            else if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                await DialogService.DisplayError(response.ErrorMessage);
            }
        }

        private void RemoveSpokenLanguageClick(int index)
        {
            _command.PersonalInformation.SpokenLanguages.RemoveAt(index);
            StateHasChanged();
        }

        private void AddSpokenLanguageClick()
        {
            _command.PersonalInformation.SpokenLanguages.Add(new SpokenLanguageDto());
            StateHasChanged();
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

        private Task PersistState()
        {
            var state = new PersonalInformationComponentState
            {
                TutorPersonalInformation = _command.PersonalInformation,
                LanguageProficiencyLevels = _languageProficiencyLevels,
                Countries = _countries,
                Languages = _languages,
                InitializationErrorMessage = _initializationErrorMessage
            };

            ApplicationState.PersistAsJson(nameof(PersonalInformationComponentState), state);
            return Task.CompletedTask;
        }

        void IDisposable.Dispose()
        {
            _persistingSubscription.Dispose();
        }
    }


    public class PersonalInformationComponentState
    {
        public TutorPersonalInformationDto TutorPersonalInformation { get; set; }
        public List<LanguageProficiencyLevelDto> LanguageProficiencyLevels { get; set; }
        public List<LanguageDto> Languages { get; set; }
        public List<CountryDto> Countries { get; set; }
        public string InitializationErrorMessage { get; set; }
    }
}

