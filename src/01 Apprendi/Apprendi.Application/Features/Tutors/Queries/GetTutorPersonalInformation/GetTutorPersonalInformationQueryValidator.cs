using Apprendi.Application.Common.Base;
using Apprendi.Application.Features.ReferenceData;
using Apprendi.Application.Features.Tutors.Queries.GetTutorPersonalInformation;
using FluentValidation;

namespace Apprendi.Application.Features.Tutors.Command.GetTutorPersonalInformation
{
    public class GetTutorPersonalInformationQueryValidator : RequestValidator<GetTutorPersonalInformationQuery>
    {
        public GetTutorPersonalInformationQueryValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid Email");
        }
    }
}
