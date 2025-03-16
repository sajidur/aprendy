using Apprendi.Application.Common.Base;
using Apprendi.Application.Features.Users;
using System.Text.Json.Serialization;

namespace Apprendi.Application.Features.SignUp.Commands.SignUpAsTutor
{
    public class SignUpAsTutorCommandResponse : Response
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TutorDto Tutor { get; set; }
    }
}
