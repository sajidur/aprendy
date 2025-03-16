using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Tutors.Command.UpdateTutorPersonalInformation
{
    public class UpdateTutorPersonalInformationCommandHandler : RequestHandler<UpdateTutorPersonalInformationCommand, UpdateTutorPersonalInformationCommandResponse>
    {
        private readonly ITutorMapper _tutorMapper;
        private readonly IApprendiDbContext _context;

        public UpdateTutorPersonalInformationCommandHandler(IResponseFactory<UpdateTutorPersonalInformationCommandResponse> responseFactory,
                                                            ITutorMapper tutorMapper,
                                                            IApprendiDbContext context)
            : base(responseFactory)
        {
            _tutorMapper = tutorMapper;
            _context = context;
        }

        public override async Task<UpdateTutorPersonalInformationCommandResponse> Handle(UpdateTutorPersonalInformationCommand request, CancellationToken cancellationToken)
        {
            var tutor = await _context
                .Tutors
                .Where(tutor => tutor.Id == request.PersonalInformation.TutorId)
                .Include(tutor => tutor.User)
                .Include(tutor => tutor.SpokenLanguages)
                .FirstAsync(cancellationToken);

            _tutorMapper.DtoToEntity(request.PersonalInformation, tutor);

            tutor.SignUpStage = TutorSignUpStage.TeachingProfile;

            await _context.SaveChangesAsync(cancellationToken);

            return Response.Success(response =>
            {
                response.SignUpStage = TutorSignUpStage.Complete;
            });
        }
    }
}
