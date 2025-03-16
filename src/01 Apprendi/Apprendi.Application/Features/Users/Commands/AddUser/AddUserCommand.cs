using Apprendi.Application.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apprendi.Application.Features.Users.Commands.AddUser
{
    public class AddUserCommand : Request<AddUserCommandResponse>
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}

