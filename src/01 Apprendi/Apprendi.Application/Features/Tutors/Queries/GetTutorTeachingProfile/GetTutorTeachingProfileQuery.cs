using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.Tutors.Queries.GetTutorTeachingProfile
{
    public class GetTutorTeachingProfileQuery : Request<GetTutorTeachingProfileQueryResponse>
    {
        public string Email { get; set; }
    }
}
