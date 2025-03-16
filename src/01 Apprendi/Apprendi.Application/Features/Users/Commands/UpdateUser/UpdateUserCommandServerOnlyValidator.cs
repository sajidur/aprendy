using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandServerOnlyValidator : ServerOnlyRequestValidator<UpdateUserCommand, UpdateUserCommandResponse>
    {
        private readonly IApprendiDbContext _context;

        public UpdateUserCommandServerOnlyValidator(IResponseFactory<UpdateUserCommandResponse> responseFactory,
                                                    IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _context
                .Users
                .AnyAsync(user => user.Id == request.User.Id, cancellationToken);

            if (!userExists)
            {
                return Response.NotFound("Invalid User Id");
            }
            return Response.Success();
        }
    }
}
