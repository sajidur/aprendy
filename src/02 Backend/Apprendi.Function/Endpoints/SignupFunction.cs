using Apprendi.Application.Factories;
using Apprendi.Application.Features.SignUp.Commands.SignUpAsStudent;
using Apprendi.Application.Features.SignUp.Commands.SignUpAsTutor;
using Apprendi.Application.Features.SignUp.Commands.UpdateTutorAbout;
using Apprendi.Application.Features.SignUp.Queries;
using Apprendi.Function.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Apprendi.Function.Endpoints
{

    [Route("Signup")]
    public class SignUpFunction : BaseFunction
    {
        private readonly ISender _sender;
        private readonly ILogger<UserFunction> _logger;
        private readonly IResponseFactory<UnexpectedErrorResponse> _responseFactory;

        public SignUpFunction(ISender sender, ILogger<UserFunction> logger, IResponseFactory<UnexpectedErrorResponse> responseFactory) : base(sender, logger, responseFactory)
        {
            _sender = sender;
            _logger = logger;
            _responseFactory = responseFactory;
        }

        [Function(nameof(SignUpAsStudentCommand))]
        public async Task<IActionResult> SignUpAsStudentCommand([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<SignUpAsStudentCommand>(req, cancellationToken);
        }

        [Function(nameof(SignUpAsTutorCommand))]
        public async Task<IActionResult> SignUpAsTutorCommand([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<SignUpAsTutorCommand>(req, cancellationToken);
        }

        [Function(nameof(UpdateTutorAboutCommand))]
        public async Task<IActionResult> UpdateTutorAboutCommand([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<UpdateTutorAboutCommand>(req, cancellationToken);
        }

        [Function(nameof(GetTutorSignUpStageQuery))]
        public async Task<IActionResult> GetTutorSignUpStageQuery([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<GetTutorSignUpStageQuery>(req, cancellationToken);
        }
    }
}
