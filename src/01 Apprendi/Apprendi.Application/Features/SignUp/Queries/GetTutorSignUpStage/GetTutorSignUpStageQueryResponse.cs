using Apprendi.Application.Common.Base;
using Apprendi.Domain.Enums;

namespace Apprendi.Application.Features.SignUp.Queries
{
    public class GetTutorSignUpStageQueryResponse : Response
    {
        public TutorSignUpStage SignUpStage { get; set; }
    }
}
