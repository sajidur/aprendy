using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.Users.Queries.GetUserByEmail;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Users.Commands.UpdateUser
{
    public class GetUserByEmailQueryServerOnlyValidator : ServerOnlyRequestValidator<GetUserByEmailQuery, GetUserByEmailQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetUserByEmailQueryServerOnlyValidator(IResponseFactory<GetUserByEmailQueryResponse> responseFactory,
                                                    IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<GetUserByEmailQueryResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var userExists = await _context
                .Users
                .AnyAsync(user => user.Email == request.Email, cancellationToken);

            if (!userExists)
            {
                return Response.NotFound("User not found");
            }

            return Response.Success();
        }
    }
}
