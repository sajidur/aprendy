using Apprendi.Domain.Enums;
using System.Text.Json.Serialization;

namespace Apprendi.Application.Features.Users
{
    public class TutorInformationDto
    {
        public int TutorId { get; set; }
        public TutorSignUpStage TutorSignUpStage { get; set; }
    }
}
