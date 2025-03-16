namespace Apprendi.Domain.Entities
{
    public class Country
    {
        public string Id { get; set; }
        public string CountryCode => Id;

        public string CountryName { get; set; }

        public ICollection<TimeZone> TimeZones { get; set; }
    }
}

