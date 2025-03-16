using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.ReferenceData.Queries.GetTimeZones
{
    public class GetTimeZonesQueryResponse : Response
    {
        public List<TimeZoneDto> TimeZones { get; set; } = new();
    }
}
