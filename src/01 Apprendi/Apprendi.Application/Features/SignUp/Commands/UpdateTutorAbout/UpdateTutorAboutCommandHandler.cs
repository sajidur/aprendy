using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.SignUp.Commands.UpdateTutorAbout;
using Apprendi.Domain.Entities;
using Apprendi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.SignUp.Commands.RegisterUserAsStudent
{
    public class UpdateTutorAboutCommandHandler : RequestHandler<UpdateTutorAboutCommand, UpdateTutorAboutCommandResponse>
    {
        private readonly IApprendiDbContext _context;
        private readonly ISignupMapper _mapper;

        public UpdateTutorAboutCommandHandler(IResponseFactory<UpdateTutorAboutCommandResponse> responseFactory,
                                              IApprendiDbContext context,
                                              ISignupMapper mapper) : base(responseFactory)
        {
            _context = context;
            _mapper = mapper;
        }

        

        public override async Task<UpdateTutorAboutCommandResponse> Handle(UpdateTutorAboutCommand request, CancellationToken cancellationToken)
        {
            using var transation = await _context.BeginTransactionAsync(cancellationToken);
            
            await RemoveTutorRelatedData(request, cancellationToken);

            var tutor = await _context.Tutors
                .Where(tutor => tutor.Id == request.TutorId)
                .Include(tutor => tutor.User)
                .FirstAsync(cancellationToken);

            _mapper.DtoToEntity(request, tutor);

            SetNextSignUpStage(tutor);

            await _context.SaveChangesAsync(cancellationToken);

            await transation.CommitAsync(cancellationToken);

            return Response.Success();
        }

        private static void SetNextSignUpStage(Tutor tutor)
        {
            //tutor.SignUpStage = TutorSignUpStage.Photo;
        }

        private async Task RemoveTutorRelatedData(UpdateTutorAboutCommand request, CancellationToken cancellationToken)
        {
            await _context.Set<TutorSubject>()
                            .Where(tutorSubject => tutorSubject.TutorId == request.TutorId)
                            .ExecuteDeleteAsync(cancellationToken);

            await _context.Set<SpokenLanguage>()
                .Where(tutorLanguage => tutorLanguage.TutorId == request.TutorId)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
