using Apprendi.Application.Common.Base;
using Apprendi.Application.Features.ReferenceData;

namespace Apprendi.Application.Features.Tutors.Queries.GetTutorPersonalInformation
{
    public class GetTutorPersonalInformationQueryResponse : Response
    {
        public TutorPersonalInformationDto TutorPersonalInformation { get; set; }
        public List<LanguageProficiencyLevelDto> LanguageProficiencyLevels { get; set; }
        public List<LanguageDto> Languages { get; set; }
        public List<CountryDto> Countries { get; set; }
    }
}
