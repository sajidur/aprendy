using Apprendi.Application.Features.ReferenceData;
using Apprendi.Application.Features.Tutors;
using Apprendi.Application.Features.Tutors.Command.UpdateTutorTeachingProfile;
using Apprendi.Application.Features.Tutors.Queries.GetTutorTeachingProfile;
using Apprendi.Application.Features.Users;
using Apprendi.Web.Client.Services;
using Apprendi.Web.Client.Services.ApiRequestClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace Apprendi.Web.Client.Pages.SignUp.Tutor
{
    public partial class TeachingProfile : ComponentBase, IDisposable
    {
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] private PersistentComponentState ApplicationState { get; set; }
        [Inject] private IApiRequestClient ApiRequestClient { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private PersistingComponentStateSubscription _persistingSubscription;

        private readonly UpdateTutorTeachingProfileCommand _command = new();
        private string _initializationErrorMessage;

        private List<SubjectDto> _teachingSubjects = new();
        private List<TeachingCertificateDto> _teachingCertificates = new();
        private List<TeachingMaterialDto> _teachingMaterials = new();
        private List<TeachingPreferenceDto> _teachingPreferences = new();
        private List<int> _numberOfYearsOfTeachingExperience = Enumerable.Range(0, 50).ToList();
        private bool _isAddVideo;
        private bool _isVideoAdded;
        private string _videoUrl;

        [CascadingParameter]
        public UserDto CurrentUser { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _persistingSubscription = ApplicationState.RegisterOnPersisting(PersistState);

            if (ApplicationState.TryTakeFromJson<TeachingProfileComponentState>(nameof(TeachingProfileComponentState), out var state))
            {
                _command.TeachingProfile = state.TeachingProfile;
                _teachingSubjects = state.TeachingSubjects;
                _teachingCertificates = state.TeachingCertificates;
                _teachingMaterials = state.TeachingMaterials;
                _teachingPreferences = state.TeachingPreferences;
                _initializationErrorMessage = state.InitializationErrorMessage;
            }
            else
            {
                var email = await GetEmail();

                var response = await ApiRequestClient.Send(new GetTutorTeachingProfileQuery { Email = email });

                if (response.IsSuccess)
                {
                    _command.TeachingProfile = response.TeachingProfile;
                    _teachingSubjects = response.TeachingSubjects;
                    _teachingCertificates = response.TeachingCertificates;
                    _teachingMaterials = response.TeachingMaterials;
                    _teachingPreferences = response.TeachingPreferences;
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

        private void OnGoBackClick()
        {
            NavigationManager.NavigateTo("/signup/tutor/personal-information");
        }

        private string GetTeachingMaterialClass(int teachingMaterialId)
        {
            return _command.TeachingProfile.TeachingMaterialsIds.Contains(teachingMaterialId) ? "selected" : "";
        }

        private string GetTeachingPreferenceClass(int teachingPreferenceId)
        {
            return _command.TeachingProfile.TeachingPreferencesIds.Contains(teachingPreferenceId) ? "selected" : "";
        }

        private void OnAddVideoUrlClick()
        {
            _isAddVideo = true;
            _isVideoAdded = false;
        }
        private void OnShowVideoUrlClick()
        {
            _isAddVideo = false;
            _isVideoAdded = true;
        }

        private void OnRemoveVideoClick()
        {
            _videoUrl = "";
            _isAddVideo = false;
            _isVideoAdded = false;
        }

        private void ToggleTeachingMaterialSelection(int teachingMaterialId)
        {
            if (!_command.TeachingProfile.TeachingMaterialsIds.Remove(teachingMaterialId))
            {
                _command.TeachingProfile.TeachingMaterialsIds.Add(teachingMaterialId);
            }
        }

        private void ToggleTeachingPreferenceSelection(int teachingPreferenceId)
        {
            if (!_command.TeachingProfile.TeachingPreferencesIds.Remove(teachingPreferenceId))
            {
                _command.TeachingProfile.TeachingPreferencesIds.Add(teachingPreferenceId);
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

        private async Task OnCreateTutorProfileClick()
        {
            var response = await ApiRequestClient.Send(_command);

            if (response.IsSuccess)
            {
                await DialogService.DisplayMessage("Your profile has been created successfully.", "Success");
                NavigationManager.NavigateTo("/");
            }
            else if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                await DialogService.DisplayError(response.ErrorMessage);
            }
        }

        private Task PersistState()
        {
            var state = new TeachingProfileComponentState
            {
                TeachingProfile = _command.TeachingProfile,
                TeachingCertificates = _teachingCertificates,
                TeachingMaterials = _teachingMaterials,
                TeachingPreferences = _teachingPreferences,
                TeachingSubjects = _teachingSubjects,
                InitializationErrorMessage = _initializationErrorMessage
            };

            ApplicationState.PersistAsJson(nameof(TeachingProfileComponentState), state);
            return Task.CompletedTask;
        }

        void IDisposable.Dispose()
        {
            _persistingSubscription.Dispose();
        }
    }


    public class TeachingProfileComponentState
    {
        public TutorTeachingProfileDto TeachingProfile { get; set; }
        public List<SubjectDto> TeachingSubjects { get; set; } = new();
        public List<TeachingCertificateDto> TeachingCertificates { get; set; } = new();
        public List<TeachingMaterialDto> TeachingMaterials { get; set; } = new();
        public List<TeachingPreferenceDto> TeachingPreferences { get; set; } = new();
        public string InitializationErrorMessage { get; set; }
    }
}

