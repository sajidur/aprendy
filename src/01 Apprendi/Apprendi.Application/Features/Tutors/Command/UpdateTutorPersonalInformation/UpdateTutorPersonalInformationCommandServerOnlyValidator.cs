using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Tutors.Command.UpdateTutorPersonalInformation
{
    public class UpdateTutorPersonalInformationCommandServerOnlyValidator : ServerOnlyRequestValidator<UpdateTutorPersonalInformationCommand, UpdateTutorPersonalInformationCommandResponse>
    {
        private readonly IApprendiDbContext _context;

        public UpdateTutorPersonalInformationCommandServerOnlyValidator(IResponseFactory<UpdateTutorPersonalInformationCommandResponse> responseFactory,
                                                                        IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<UpdateTutorPersonalInformationCommandResponse> Handle(UpdateTutorPersonalInformationCommand request, CancellationToken cancellationToken)
        {
            var personalInformation = request.PersonalInformation;

            //tutor
            var exists = await _context
                .Tutors
                .AnyAsync(tutor => tutor.Id == personalInformation.TutorId, cancellationToken);

            if (!exists) return Response.NotFound("Tutor not found");

            
            //country of birth
            var countryExists = await _context
                .Countries
                .AnyAsync(country => country.Id == personalInformation.CountryOfBirthId, cancellationToken);

            if (!countryExists) return Response.NotFound("Country of birth not found");

            //country of residence
            countryExists = await _context
                .Countries
                .AnyAsync(country => country.Id == personalInformation.CountryResidencyId, cancellationToken);

            if (!countryExists) return Response.NotFound("Country of residence not found");


            // Language Spoken
            var languageIds = personalInformation.SpokenLanguages.Select(x => x.LanguageId).ToList();
            var levelIds = personalInformation.SpokenLanguages.Select(x => x.ProficiencyLevelId).ToList();

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
