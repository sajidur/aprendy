using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Domain.Entities;
using MediatR.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : RequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
    {
        private readonly IApprendiDbContext _context;

        public UpdateUserCommandHandler(IResponseFactory<UpdateUserCommandResponse> responseFactory, 
                                     IApprendiDbContext context,
                                     IUserMapper userMapper) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = request.User;
            
            await _context.Users
                    .Where(user => user.Id == request.User.Id)
                    .ExecuteUpdateAsync(user => user.SetProperty(u => u.FirstName, userDto.FirstName)
                                                    .SetProperty(u => u.LastName, userDto.LastName)
                                                    .SetProperty(u => u.Email, userDto.Email), cancellationToken: cancellationToken);
            

            var updateUserDto = await _context.Users
                .AsNoTracking()
                .Where(user => user.Id == request.User.Id)
                .ProjectToDto()
                .SingleAsync(cancellationToken);

            return Response.Success(response =>
            {
                response.User = updateUserDto;
            });
        }
    }
}
