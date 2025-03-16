using Apprendi.Application.Common.Base;
using Apprendi.Domain.Enums;

namespace Apprendi.Application.Features.Tutors.Command.UpdateTutorTeachingProfile
{
    public class UpdateTutorTeachingProfileCommandResponse : Response
    {
        public TutorSignUpStage SignUpStage { get; set; }
    }
}
