using Apprendi.Application.Common.Base;
using System.Text.Json.Serialization;

namespace Apprendi.Application.Features.Users.Commands.AddUser
{
    public class AddUserCommandResponse : Response
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public UserDto User { get; set; }
    }
}
