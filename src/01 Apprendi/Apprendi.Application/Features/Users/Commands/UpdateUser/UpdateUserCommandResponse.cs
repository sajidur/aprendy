using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandResponse : Response
    {
        public UserDto User { get; set; }
    }
}
