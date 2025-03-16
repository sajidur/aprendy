using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.SignUp.Queries
{
    public class GetTutorAboutStageDetailsQuery : Request<GetTutorAboutStageDetailsQueryResponse>
    {
        public string Email { get; set; }
    }
}
