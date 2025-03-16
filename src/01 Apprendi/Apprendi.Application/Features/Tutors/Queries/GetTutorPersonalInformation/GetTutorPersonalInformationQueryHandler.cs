using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.ReferenceData;
using Apprendi.Application.Features.Tutors;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Tutors.Queries.GetTutorPersonalInformation
{
    public class GetTutorPersonalInformationQueryHandler : RequestHandler<GetTutorPersonalInformationQuery, GetTutorPersonalInformationQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetTutorPersonalInformationQueryHandler(IResponseFactory<GetTutorPersonalInformationQueryResponse> responseFactory,
                                          IApprendiDbContext context)
            : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<GetTutorPersonalInformationQueryResponse> Handle(GetTutorPersonalInformationQuery request, CancellationToken cancellationToken)
        {
            var tutorPersonalInformation = await _context
                .Tutors
                .Where(tutor => tutor.User.Email == request.Email)
                .AsNoTracking()                
                .ProjectToTutorPersonalInformationDto()
                .FirstAsync(cancellationToken);

            var languages = await _context.Languages.AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            var subjects = await _context.Subjects.AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            var countries = await _context.Countries.AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            var languageProficiencyLevels = await _context.LanguageProficiencyLevels.AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            NullFirstNameIfEmptyOrWhiteSpace(tutorPersonalInformation);

            return Response.Success(response =>
            {
                response.TutorPersonalInformation = tutorPersonalInformation;
                response.Countries = countries;
                response.Languages = languages;
                response.LanguageProficiencyLevels = languageProficiencyLevels;
            });
        }

        private void NullFirstNameIfEmptyOrWhiteSpace(TutorPersonalInformationDto tutorPersonalInformation)
        {
            //When setting up the account in Entra ID, it doesn't allow empty string, so we set it up with a space
            //we need to null if it's a space
            if (string.IsNullOrWhiteSpace(tutorPersonalInformation.FirstName))
            {
                tutorPersonalInformation.FirstName = null;
            }
        }
    }
}
