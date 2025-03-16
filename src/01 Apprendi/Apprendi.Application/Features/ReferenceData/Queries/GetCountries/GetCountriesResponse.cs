using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.ReferenceData.Queries.GetCountries
{
    public class GetCountriesResponse : Response
    {
        public List<CountryDto> Countries { get; set; } = new();
    }
}
