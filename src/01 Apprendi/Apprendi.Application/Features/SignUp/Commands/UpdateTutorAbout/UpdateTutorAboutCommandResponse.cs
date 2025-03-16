using Apprendi.Application.Common.Base;
using Apprendi.Application.Features.Users;
using System.Text.Json.Serialization;

namespace Apprendi.Application.Features.SignUp.Commands.UpdateTutorAbout
{
    public class UpdateTutorAboutCommandResponse : Response
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public UserDto User { get; set; }
    }
}
