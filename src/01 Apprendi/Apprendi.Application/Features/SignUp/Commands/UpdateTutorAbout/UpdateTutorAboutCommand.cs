using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.SignUp.Commands.UpdateTutorAbout
{
    public class UpdateTutorAboutCommand : Request<UpdateTutorAboutCommandResponse>
    {
        public int TutorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryOfBirthId { get; set; }
        public List<int> SubjectsTaughtIds { get; set; } = new();
        public List<SpokenLanguageDto> SpokenLanguages { get; set; } = new();
        public bool IsOver18 { get; set; }
    }
}

