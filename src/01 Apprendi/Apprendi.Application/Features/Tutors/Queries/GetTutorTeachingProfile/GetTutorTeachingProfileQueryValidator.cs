using Apprendi.Application.Common.Base;
using FluentValidation;

namespace Apprendi.Application.Features.Tutors.Queries.GetTutorTeachingProfile
{
    public class GetTutorTeachingProfileQueryValidator : RequestValidator<GetTutorTeachingProfileQuery>
    {
        public GetTutorTeachingProfileQueryValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid Email");
        }
    }
}
