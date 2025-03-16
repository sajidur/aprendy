using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Domain.Entities;
using Apprendi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Tutors.Command.UpdateTutorTeachingProfile
{
    public class UpdateTutorTeachingProfileCommandHandler : RequestHandler<UpdateTutorTeachingProfileCommand, UpdateTutorTeachingProfileCommandResponse>
    {
        private readonly ITutorMapper _tutorMapper;
        private readonly IApprendiDbContext _context;

        public UpdateTutorTeachingProfileCommandHandler(IResponseFactory<UpdateTutorTeachingProfileCommandResponse> responseFactory,
                                                        ITutorMapper tutorMapper,
                                                        IApprendiDbContext context)
            : base(responseFactory)
        {
            _tutorMapper = tutorMapper;
            _context = context;
        }

        public override async Task<UpdateTutorTeachingProfileCommandResponse> Handle(UpdateTutorTeachingProfileCommand request, CancellationToken cancellationToken)
        {
            using var transation = await _context.BeginTransactionAsync(cancellationToken);

            var tutor = await _context
                .Tutors
                .Where(tutor => tutor.Id == request.TeachingProfile.TutorId)
                .Include(tutor => tutor.User)
                .FirstAsync(cancellationToken);

            await RemoveTutorRelatedData(tutor, cancellationToken);

            _tutorMapper.DtoToEntity(request.TeachingProfile, tutor);

            tutor.SignUpStage = TutorSignUpStage.Complete;

            await _context.SaveChangesAsync(cancellationToken);

            await transation.CommitAsync(cancellationToken);

            return Response.Success(response =>
            {
                response.SignUpStage = TutorSignUpStage.Complete;
            });
        }

        private async Task RemoveTutorRelatedData(Tutor tutor, CancellationToken cancellationToken)
        {
            await _context
                .Set<TutorSubject>()
                .Where(tutorSubject => tutorSubject.TutorId == tutor.Id)
                .ExecuteDeleteAsync(cancellationToken);

            await _context
                .Set<TutorTeachingCertificate>()
                .Where(tutorTeachingCertificate => tutorTeachingCertificate.TutorId == tutor.Id)
                .ExecuteDeleteAsync(cancellationToken);

            await _context
                .Set<TutorTeachingMaterial>()
                .Where(tutorTeachingMaterial => tutorTeachingMaterial.TutorId == tutor.Id)
                .ExecuteDeleteAsync(cancellationToken);

            await _context
                .Set<TutorTeachingPreference>()
                .Where(tutorTeachingPreference => tutorTeachingPreference.TutorId == tutor.Id)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
