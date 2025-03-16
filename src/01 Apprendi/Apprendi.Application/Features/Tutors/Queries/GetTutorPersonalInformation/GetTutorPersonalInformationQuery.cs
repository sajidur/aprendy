using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.Tutors.Queries.GetTutorPersonalInformation
{
    public class GetTutorPersonalInformationQuery : Request<GetTutorPersonalInformationQueryResponse>
    {
        public string Email { get; set; }
    }
}
