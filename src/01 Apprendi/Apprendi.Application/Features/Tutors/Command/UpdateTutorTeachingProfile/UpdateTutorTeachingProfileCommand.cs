using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.Tutors.Command.UpdateTutorTeachingProfile
{
    public class UpdateTutorTeachingProfileCommand : Request<UpdateTutorTeachingProfileCommandResponse>
    {
        public TutorTeachingProfileDto TeachingProfile { get; set; } = new();
    }
}
