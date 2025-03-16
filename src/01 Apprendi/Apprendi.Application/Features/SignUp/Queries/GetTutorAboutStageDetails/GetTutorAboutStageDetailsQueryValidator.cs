using Apprendi.Application.Features.SignUp.Queries;
using FluentValidation;

namespace Apprendi.Application.Features.Users.Commands.UpdateUser
{
    public class GetTutorAboutStageDetailsQueryValidator : AbstractValidator<GetTutorAboutStageDetailsQuery>
    {
        public GetTutorAboutStageDetailsQueryValidator()
        {
            RuleFor(request => request.Email)
                .NotNull()
                .WithMessage("Required");
        }
    }
}
