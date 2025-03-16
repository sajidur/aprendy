using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryResponse : Response
    {
        public List<UserDto> Users { get; set; } = new();
    }
}
