using FluentValidation;

namespace Apprendi.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(request => request.User)
                .NotNull();

            RuleFor(request => request.User)
                .SetValidator(new UserDtoValidator());
        }
    }
}
