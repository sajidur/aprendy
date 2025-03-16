using Apprendi.Application.Common.Base;
using System.Text.Json.Serialization;

namespace Apprendi.Application.Features.ReferenceData.Queries.GetReferenceData
{
    public class GetReferenceDataQueryResponse : Response
    {
        public List<RoleDto> Roles { get; set; } = new();
        public List<LanguageDto> Languages { get; set; } = new();
        public List<CurrencyDto> Currencies { get; set; } = new();
    }
}
