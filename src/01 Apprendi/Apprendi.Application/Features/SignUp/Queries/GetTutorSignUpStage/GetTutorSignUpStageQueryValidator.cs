using Apprendi.Application.Features.SignUp.Queries;
using FluentValidation;

namespace Apprendi.Application.Features.Users.Commands.UpdateUser
{
    public class GetTutorSignUpStageQueryValidator : AbstractValidator<GetTutorSignUpStageQuery>
    {
        public GetTutorSignUpStageQueryValidator()
        {
            RuleFor(request => request.TutorId)
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0");
        }
    }
}
