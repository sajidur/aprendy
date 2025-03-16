using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;

namespace Apprendi.Application.Features.Users.Commands.AddUser
{
    public class AddUserCommandHandler : RequestHandler<AddUserCommand, AddUserCommandResponse>
    {
        private readonly IApprendiDbContext _context;
        private readonly IUserMapper _userMapper;

        public AddUserCommandHandler(IResponseFactory<AddUserCommandResponse> responseFactory, 
                                     IApprendiDbContext context,
                                     IUserMapper userMapper) : base(responseFactory)
        {
            _context = context;
            _userMapper = userMapper;
        }

        public override async Task<AddUserCommandResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userMapper.ToEntity(request);

            await _context
                .Users
                .AddAsync(user, cancellationToken);

            await _context.SaveChangesAsync();

            var userDto = _userMapper.ToDto(user);

            return Response.Success(response =>
            {
                response.User = userDto;
            });
        }
    }
}
