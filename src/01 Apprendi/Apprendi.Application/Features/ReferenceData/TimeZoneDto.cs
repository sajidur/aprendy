namespace Apprendi.Application.Features.ReferenceData
{
    public class TimeZoneDto
    {
        public string Id { get; set; }
        public string Name => Id;
        public string Abbreviation { get; set; }
        public double Offset { get; set; }        
    }
}
