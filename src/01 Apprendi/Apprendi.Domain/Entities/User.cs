using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apprendi.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CountryCode { get; set; }
        public string ProfilePictureURL { get; set; }
        public string TimeZoneId { get; set; }
        public TimeZone TimeZone { get; set; }
        public int? CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int? LanguageId { get; set; }
        public Language Language { get; set; }
        public List<UserRole> Roles { get; set; } = new();
    }
}
