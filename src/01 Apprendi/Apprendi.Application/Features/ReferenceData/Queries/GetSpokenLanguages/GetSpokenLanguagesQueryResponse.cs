using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.ReferenceData.Queries.GetSpokenLanguages
{
    public class GetSpokenLanguagesQueryResponse : Response
    {
        public List<SpokenLanguageDto> SpokenLanguages { get; set; } = new();
    }
}
