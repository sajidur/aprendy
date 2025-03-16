using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Tutors.Command.UpdateTutorTeachingProfile
{
    public class UpdateTutorTeachingProfileServerOnlyValidator : ServerOnlyRequestValidator<UpdateTutorTeachingProfileCommand, UpdateTutorTeachingProfileCommandResponse>
    {
        private readonly IApprendiDbContext _context;

        public UpdateTutorTeachingProfileServerOnlyValidator(IResponseFactory<UpdateTutorTeachingProfileCommandResponse> responseFactory,
                                                                        IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<UpdateTutorTeachingProfileCommandResponse> Handle(UpdateTutorTeachingProfileCommand request, CancellationToken cancellationToken)
        {
            var teachingProfile = request.TeachingProfile;

            var exists = await _context
                .Tutors
                .AnyAsync(tutor => tutor.Id == teachingProfile.TutorId, cancellationToken);

            if (!exists) return Response.NotFound("Tutor not found");

            //Teaching Subjects
            var count = await _context.Subjects
                .Where(subject => teachingProfile.TeachingSubjectsIds.Contains(subject.Id))
                .CountAsync(cancellationToken);

            if (count != teachingProfile.TeachingSubjectsIds.Count)
                return Response.NotFound("One or more teaching subjects are invalid");

            //Teaching Certificates
            count = await _context.TeachingCertificates
                .Where(teachingCertificate => teachingProfile.TeachingCertificatesIds.Contains(teachingCertificate.Id))
                .CountAsync(cancellationToken);

            if (count != teachingProfile.TeachingCertificatesIds.Count)
                return Response.NotFound("One or more teaching certificates are invalid");

            //Teaching Materials
            count = await _context.TeachingMaterials
                .Where(teachingMaterials => teachingProfile.TeachingMaterialsIds.Contains(teachingMaterials.Id))
                .CountAsync(cancellationToken);

            if (count != teachingProfile.TeachingMaterialsIds.Count)
                return Response.NotFound("One or more teaching materials are invalid");

            //Teaching Preferences
            count = await _context.TeachingPreferences
                .Where(teachingPreference => teachingProfile.TeachingPreferencesIds.Contains(teachingPreference.Id))
                .CountAsync(cancellationToken);

            if (count != teachingProfile.TeachingPreferencesIds.Count)
                return Response.NotFound("One or more teaching preferences are invalid");

            return Response.Success();
        }
    }
}
