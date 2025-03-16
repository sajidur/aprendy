using Apprendi.Application.Features.Users.Queries.GetUserByEmail;
using FluentValidation;

namespace Apprendi.Application.Features.Users.Commands.UpdateUser
{
    public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailQueryValidator()
        {
            RuleFor(request => request.Email)
                .EmailAddress()
                .NotNull();
        }
    }
}
