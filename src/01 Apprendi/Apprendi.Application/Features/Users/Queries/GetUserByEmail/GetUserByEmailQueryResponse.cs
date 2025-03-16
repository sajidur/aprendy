using Apprendi.Application.Common.Base;
using System.Text.Json.Serialization;

namespace Apprendi.Application.Features.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryResponse : Response
    {
        public UserDto User { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TutorInformationDto TutorInformation { get; set; }
    }
}
