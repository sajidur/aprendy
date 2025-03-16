namespace Apprendi.Domain.Entities
{
    public class TimeZone
    {
        public string Id { get; set; }
        public string Name => Id;
        public string CountryId { get; set; }
        public Country Country { get; set; }
    }
}

