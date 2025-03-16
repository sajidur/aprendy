using Apprendi.Application.Common.Base;
using FluentValidation;

namespace Apprendi.Application.Features.SignUp.Commands.UpdateTutorAbout
{
    public class UpdateTutorAboutCommandValidator : RequestValidator<UpdateTutorAboutCommand>
    {
        public UpdateTutorAboutCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Required")
                .MaximumLength(100)
                .WithMessage("Cannot exceed 100 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Required")
                .MaximumLength(100)
                .WithMessage("Cannot exceed 100 characters");

            RuleFor(x => x.CountryOfBirthId)
                .NotEmpty()
                .WithMessage("Required");

            RuleFor(x => x.SubjectsTaughtIds)
                .NotEmpty()
                .WithMessage("Required");

            RuleFor(x => x.SpokenLanguages)
                .NotEmpty()
                .WithMessage("Specify at least one language");


            RuleForEach(x => x.SpokenLanguages)
                .SetValidator(new SpokenLanguageDtoValidator());

            RuleFor(x => x.IsOver18)
                .Equal(true)
                .WithMessage("You must be at least 18 years old to teach. Please check the box to confirm you meet this age requirement.");
        }
    }

    public class SpokenLanguageDtoValidator : AbstractValidator<SpokenLanguageDto>
    {
        public SpokenLanguageDtoValidator()
        {
            RuleFor(x => x.LanguageId)
                .NotNull()
                .WithMessage("Required")
                .GreaterThan(0)
                .WithMessage("Required");

            RuleFor(x => x.LevelId)
                .NotNull()
                .WithMessage("Required")
                .GreaterThan(0)
                .WithMessage("Required");
        }
    }
}
