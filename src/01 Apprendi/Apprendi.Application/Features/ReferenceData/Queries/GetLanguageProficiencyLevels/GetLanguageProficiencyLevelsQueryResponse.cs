using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.ReferenceData.Queries.GetLanguageProficiencyLevels
{
    public class GetLanguageProficiencyLevelsQueryResponse : Response
    {
        public List<LanguageProficiencyLevelDto> LanguageProficiencyLevels { get; set; } = new();
    }
}
