using Apprendi.Application.Common.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apprendi.Application.Features.Users.Commands.AddUser
{
    public class AddUserCommandValidator : RequestValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Required")
                .MaximumLength(100)
                .WithMessage("First name cannot exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Required")
                .EmailAddress()
                .WithMessage("A valid email address is required");
        }
    }
}
