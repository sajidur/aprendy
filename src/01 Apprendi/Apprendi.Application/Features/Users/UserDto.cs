using Apprendi.Domain.Enums;
using System.Text.Json.Serialization;

namespace Apprendi.Application.Features.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TimeZoneId { get; set; }
        public int? CurrencyId { get; set; }
        public List<int> RoleIds { get; set; } = new();
    }
}
