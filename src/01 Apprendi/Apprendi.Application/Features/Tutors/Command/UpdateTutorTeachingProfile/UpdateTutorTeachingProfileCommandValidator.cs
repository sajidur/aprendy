using Apprendi.Application.Common.Base;
using Apprendi.Application.Features.ReferenceData;
using FluentValidation;

namespace Apprendi.Application.Features.Tutors.Command.UpdateTutorTeachingProfile
{
    public class UpdateTutorTeachingProfileCommandValidator : RequestValidator<UpdateTutorTeachingProfileCommand>
    {
        public UpdateTutorTeachingProfileCommandValidator()
        {
            RuleFor(x => x.TeachingProfile)
                .SetValidator(new TutorTeachingProfileDtoValidator());
        }
    }

    public class TutorTeachingProfileDtoValidator : RequestValidator<TutorTeachingProfileDto>
    {
        public TutorTeachingProfileDtoValidator()
        {
            RuleFor(x => x.TeachingSubjectsIds)
                .NotEmpty()
                .WithMessage("At least one teaching subject must be specified.");

            RuleFor(x => x.TeachingExperienceInYears)
                .NotEmpty()
                .WithMessage("Required")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Must be positive value");

            RuleFor(x => x.OtherCertificates)
                .MaximumLength(500)
                .WithMessage("Cannot exceed 500 characters.")
                .NotEmpty()
                .When(x => x.IsOtherCertificateSpecified)
                .WithMessage("Required");

            RuleFor(x => x.VideoIntroductionFileName)
                .MaximumLength(255)
                .WithMessage("Cannot exceed 255 characters.");

            RuleFor(x => x.AboutMe)
                .NotEmpty()
                .WithMessage("Required")
                .MaximumLength(1000)
                .WithMessage("Cannot exceed 1000 characters.");

            RuleFor(x => x.TeachingMaterialsIds)
                .NotEmpty()
                .WithMessage("At least one teaching material must be specified.");

            RuleFor(x => x.TeachingPreferencesIds)
                .NotEmpty()
                .WithMessage("At least one teaching preference must be specified.");

            RuleFor(x => x.TeachingCertificatesIds)
                .NotEmpty()
                .WithMessage("At least one teaching certificate must be specified.");            
        }
    }
}
