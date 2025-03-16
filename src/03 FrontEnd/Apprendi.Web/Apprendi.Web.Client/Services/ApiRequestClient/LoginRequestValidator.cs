using Apprendi.Application.Common.Base;
using FluentValidation;

namespace Apprendi.Web.Client.Services.ApiRequestClient
{
    public class LoginRequestValidator : RequestValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Required")
                .MaximumLength(254)
                .WithMessage("Email is too long")
                .EmailAddress()
                .WithMessage("A valid email address is required");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Required");
        }
    }
}
