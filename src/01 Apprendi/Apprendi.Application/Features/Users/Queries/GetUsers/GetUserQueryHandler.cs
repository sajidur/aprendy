using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Apprendi.Application.Features.Users.Queries.GetUsers
{
    public class GetUserQueryHandler : RequestHandler<GetUsersQuery, GetUsersQueryResponse>
    {
        private readonly IApprendiDbContext _context;
        private readonly IUserMapper _userMapper;

        public GetUserQueryHandler(IResponseFactory<GetUsersQueryResponse> responseFactory, 
                                   IApprendiDbContext context, 
                                   IUserMapper userMapper) : base(responseFactory)
        {
            _context = context;
            _userMapper = userMapper;
        }

        public override async Task<GetUsersQueryResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context
                .Users
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return Response.Success(response =>
            {
                response.Users = _userMapper.ToDtos(users);
            });
        }
    }
}
