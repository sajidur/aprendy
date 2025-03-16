using Apprendi.Application.Factories;
using Apprendi.Application.Features.Users.Commands.AddUser;
using Apprendi.Application.Features.Users.Commands.UpdateUser;
using Apprendi.Application.Features.Users.Queries.GetUserByEmail;
using Apprendi.Application.Features.Users.Queries.GetUsers;
using Apprendi.Function.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Apprendi.Function.Endpoints
{

    [Route("Users")]
    public class UserFunction : BaseFunction
    {
        private readonly ISender _sender;
        private readonly ILogger<UserFunction> _logger;
        private readonly IResponseFactory<UnexpectedErrorResponse> _responseFactory;

        public UserFunction(ISender sender, ILogger<UserFunction> logger, IResponseFactory<UnexpectedErrorResponse> responseFactory) : base(sender, logger, responseFactory)
        {
            _sender = sender;
            _logger = logger;
            _responseFactory = responseFactory;
        }

        [Function(nameof(GetUsersQuery))]
        public async Task<IActionResult> GetUsersQuery([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            var request = new GetUsersQuery();            
            return await Result(request, cancellationToken);
        }

        [Function(nameof(AddUserCommand))]
        public async Task<IActionResult> AddUserCommand([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<AddUserCommand>(req, cancellationToken);
        }

        [Function(nameof(UpdateUserCommand))]
        public async Task<IActionResult> UpdateUserCommand([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<UpdateUserCommand>(req, cancellationToken);
        }

        [Function(nameof(UploadUserImageCommand))]
        public async Task<IActionResult> UploadUserImageCommand([HttpTrigger(AuthorizationLevel.User, "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<UpdateUserCommand>(req, cancellationToken);
        }

        [Function(nameof(GetUserByEmailQuery))]
        public async Task<IActionResult> GetUserByEmailQuery([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<GetUserByEmailQuery>(req, cancellationToken);
        }
    }
}
