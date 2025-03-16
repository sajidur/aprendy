using Apprendi.Application.Common.Base;
using FluentValidation;

namespace Apprendi.Application.Features.Users
{
    public class UserDtoValidator: RequestValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.Id)
                .NotEmpty()
                .WithMessage("Required");

            RuleFor(user => user.FirstName)
                .NotEmpty()
                .WithMessage("Required")
                .MaximumLength(100)
                .WithMessage("First name cannot exceed 100 characters");

            RuleFor(user => user.LastName)
                 .NotEmpty()
                .WithMessage("Required")
                .MaximumLength(100)
                .WithMessage("Last name cannot exceed 100 characters");

            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Required")
                .EmailAddress()
                .WithMessage("A valid email address is required");
        }
    }
}
