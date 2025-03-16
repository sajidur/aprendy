using Apprendi.Application.Common.Base;
using FluentValidation;

namespace Apprendi.Application.Features.SignUp.Commands.SignUpAsStudent
{
    public class SignUpAsStudentCommandValidator : RequestValidator<SignUpAsStudentCommand>
    {
        public SignUpAsStudentCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Required")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Required")
                .EmailAddress()
                .WithMessage("A valid email address is required");
        }
    }
}
