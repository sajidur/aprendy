using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.SignUp.Commands.UpdateTutorAbout;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.SignUp.Commands.RegisterUserAsStudent
{
    public class UpdateTutorAboutCommandServerOnlyValidator : ServerOnlyRequestValidator<UpdateTutorAboutCommand, UpdateTutorAboutCommandResponse>
    {
        private readonly IApprendiDbContext _context;

        public UpdateTutorAboutCommandServerOnlyValidator(IResponseFactory<UpdateTutorAboutCommandResponse> responseFactory,
                                                               IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<UpdateTutorAboutCommandResponse> Handle(UpdateTutorAboutCommand request, CancellationToken cancellationToken)
        {
            //tutor
            var exists = await _context
                .Tutors
                .AnyAsync(tutor => tutor.Id == request.TutorId, cancellationToken);

            if (!exists) return Response.NotFound("Tutor not found");

            
            //country
            var countryExists = await _context
                .Countries
                .AnyAsync(country => country.Id == request.CountryOfBirthId, cancellationToken);

            if (!countryExists) return Response.NotFound("Country not found");

            //subjects
            var subjectsCount = await _context
                .Subjects
                .Where(subject => request.SubjectsTaughtIds.Contains(subject.Id))
                .CountAsync(cancellationToken) ;

            var allSubjectsExist = subjectsCount == request.SubjectsTaughtIds.Count;

            if (!allSubjectsExist) return Response.NotFound("One or more subjects hasn't been found");

            // Language Spoken
            var languageIds = request.SpokenLanguages.Select(x => x.LanguageId).ToList();
            var levelIds = request.SpokenLanguages.Select(x => x.LevelId).ToList();

            var validLanguageCount = await _context.Languages
                .Where(language => languageIds.Contains(language.Id))
                .CountAsync(cancellationToken);

            var validLevelCount = await _context.LanguageProficiencyLevels
                .Where(level => levelIds.Contains(level.Id))
                .CountAsync(cancellationToken);

            if (validLanguageCount != languageIds.Count)
                return Response.NotFound("One or more language are invalid");

            if (validLevelCount != levelIds.Count)
                return Response.NotFound("One or more proficiency spoken language levels are invalid");

            return Response.Success();
        }
    }
}
