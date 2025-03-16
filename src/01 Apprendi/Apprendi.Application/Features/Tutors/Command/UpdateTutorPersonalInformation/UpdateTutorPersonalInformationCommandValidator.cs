using Apprendi.Application.Common.Base;
using Apprendi.Application.Features.ReferenceData;
using FluentValidation;

namespace Apprendi.Application.Features.Tutors.Command.UpdateTutorPersonalInformation
{
    public class UpdateTutorPersonalInformationCommandValidator : RequestValidator<UpdateTutorPersonalInformationCommand>
    {
        public UpdateTutorPersonalInformationCommandValidator()
        {
            RuleFor(x => x.PersonalInformation)
                .SetValidator(new TutorPersonalInformationDtoValidator());
        }
    }

    public class TutorPersonalInformationDtoValidator : RequestValidator<TutorPersonalInformationDto>
    {
        public TutorPersonalInformationDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Required")
                .MaximumLength(100)
                .WithMessage("Cannot exceed 100 characters");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Required")
                .MaximumLength(100)
                .WithMessage("Cannot exceed 100 characters");

            RuleFor(x => x.LastName)                
                .MaximumLength(100)
                .WithMessage("Cannot exceed 100 characters");

            RuleFor(x => x.CountryOfBirthId)
                .NotEmpty()
                .WithMessage("Required");

            RuleFor(x => x.CountryResidencyId)
                .NotEmpty()
                .WithMessage("Required");

            RuleFor(x => x.DateOfBirth)
                .Must(MustBeOver18)
                .WithMessage("Must be at least 18.");

            RuleFor(x => x.SpokenLanguages)
                .NotEmpty()
                .WithMessage("Specify at least one language");

            RuleFor(x => x.IsPhotoPolicyAgreed)
                .Equal(true)
                .WithMessage("You must agree to the photo policy.");


            RuleForEach(x => x.SpokenLanguages)
                .SetValidator(new SpokenLanguageDtoValidator());
        }

        private bool MustBeOver18(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            return age >= 18;
        }
    }

    public class SpokenLanguageDtoValidator : AbstractValidator<SpokenLanguageDto>
    {
        public SpokenLanguageDtoValidator()
        {
            RuleFor(x => x.LanguageId)
                .GreaterThan(0)
                .WithMessage("Required");

            RuleFor(x => x.ProficiencyLevelId)
                .GreaterThan(0)
                .WithMessage("Required");
        }
    }
}
