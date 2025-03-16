using Apprendi.Application.Common.Base;
using Apprendi.Domain.Enums;

namespace Apprendi.Application.Features.Tutors.Command.UpdateTutorPersonalInformation
{
    public class UpdateTutorPersonalInformationCommandResponse : Response
    {
        public TutorSignUpStage SignUpStage { get; set; }
    }
}
