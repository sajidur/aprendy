using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.ReferenceData;
using Apprendi.Application.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.SignUp.Queries.GetTutorSignUpStage
{
    public class GetTutorAboutStageDetailsQueryHandler : RequestHandler<GetTutorAboutStageDetailsQuery, GetTutorAboutStageDetailsQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetTutorAboutStageDetailsQueryHandler(IResponseFactory<GetTutorAboutStageDetailsQueryResponse> responseFactory,
                                          IApprendiDbContext context)
            : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<GetTutorAboutStageDetailsQueryResponse> Handle(GetTutorAboutStageDetailsQuery request, CancellationToken cancellationToken)
        {
            var tutorDetails = await _context
                .Tutors
                .Where(tutor => tutor.User.Email == request.Email)
                .AsNoTracking()                
                .ProjectToAboutDetailsDto()
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

            return Response.Success(response =>
            {
                response.TutorDetails = tutorDetails;
                response.Subjects = subjects;
                response.Countries = countries;
                response.SpokenLanguages = languages;
                response.LanguageProficiencyLevels = languageProficiencyLevels;
            });
        }
    }
}
