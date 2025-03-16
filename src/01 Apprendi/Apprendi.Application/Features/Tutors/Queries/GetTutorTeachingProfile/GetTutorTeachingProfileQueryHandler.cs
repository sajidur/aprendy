using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.ReferenceData;
using Apprendi.Application.Features.Tutors;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Tutors.Queries.GetTutorTeachingProfile
{
    public class GetTutorTeachingProfileQueryHandler : RequestHandler<GetTutorTeachingProfileQuery, GetTutorTeachingProfileQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetTutorTeachingProfileQueryHandler(IResponseFactory<GetTutorTeachingProfileQueryResponse> responseFactory,
                                                   IApprendiDbContext context)
            : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<GetTutorTeachingProfileQueryResponse> Handle(GetTutorTeachingProfileQuery request, CancellationToken cancellationToken)
        {
            var teachingProfile = await _context
                .Tutors
                .Where(tutor => tutor.User.Email == request.Email)
                .AsNoTracking()                
                .ProjectToTutorTeachingProfileDto()
                .FirstAsync(cancellationToken);

            var teachingCertificates = await _context.TeachingCertificates.AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            var teachingSubjects = await _context.Subjects.AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            var teachingPreferences = await _context.TeachingPreferences.AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            var teachingMaterials = await _context.TeachingMaterials.AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);


            return Response.Success(response =>
            {
                response.TeachingProfile = teachingProfile;
                response.TeachingPreferences = teachingPreferences;
                response.TeachingCertificates = teachingCertificates;
                response.TeachingMaterials = teachingMaterials;
                response.TeachingSubjects = teachingSubjects;
            });
        }
    }
}
