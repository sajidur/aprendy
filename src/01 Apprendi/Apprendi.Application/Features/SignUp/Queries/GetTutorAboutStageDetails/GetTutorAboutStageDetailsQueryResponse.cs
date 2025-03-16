using Apprendi.Application.Common.Base;
using Apprendi.Application.Features.ReferenceData;

namespace Apprendi.Application.Features.SignUp.Queries
{
    public class GetTutorAboutStageDetailsQueryResponse : Response
    {
        public List<SubjectDto> Subjects { get; set; } = new();
        public List<CountryDto> Countries { get; set; } = new();
        public List<LanguageDto> SpokenLanguages { get; set; } = new();
        public List<LanguageProficiencyLevelDto> LanguageProficiencyLevels { get; set; } = new();
        public TutorAboutStageDetailsDto TutorDetails { get; set; } = new();
    }
}
