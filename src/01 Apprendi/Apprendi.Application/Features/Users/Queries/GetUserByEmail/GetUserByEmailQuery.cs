using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : Request<GetUserByEmailQueryResponse>
    {
        public string Email { get; set; }
    }
}
