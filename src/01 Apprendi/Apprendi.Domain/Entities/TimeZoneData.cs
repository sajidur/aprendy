namespace Apprendi.Domain.Entities
{
    public class TimeZoneData
    {
        public string TimeZoneId => ZoneName;
        public string ZoneName { get; set; }
        public string CountryCode { get; set; }
        public string Abbreviation { get; set; }
        public long TimeStart { get; set; }
        public long GmtOffset { get; set; }
        public string DaylightSavingTime { get; set; }
    }
}

