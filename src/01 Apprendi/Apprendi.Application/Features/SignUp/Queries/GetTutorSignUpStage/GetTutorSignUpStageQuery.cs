using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.SignUp.Queries
{
    public class GetTutorSignUpStageQuery : Request<GetTutorSignUpStageQueryResponse>
    {
        public int TutorId { get; set; }
    }
}
