using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.Tutors.Command.UpdateTutorPersonalInformation
{
    public class UpdateTutorPersonalInformationCommand : Request<UpdateTutorPersonalInformationCommandResponse>
    {
        public TutorPersonalInformationDto PersonalInformation { get; set; } = new();
    }
}
