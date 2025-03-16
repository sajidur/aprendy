using Apprendi.Application.Factories;
using Apprendi.Application.Features.Tutors.Command.UpdateTutorPersonalInformation;
using Apprendi.Application.Features.Tutors.Queries.GetTutorPersonalInformation;
using Apprendi.Application.Features.Tutors.Queries.GetTutorTeachingProfile;
using Apprendi.Application.Features.Tutors.Command.UpdateTutorTeachingProfile;
using Apprendi.Function.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Apprendi.Function.Endpoints
{

    [Route("Tutors")]
    public class TutorFunction : BaseFunction
    {
        public TutorFunction(ISender sender, ILogger<UserFunction> logger, IResponseFactory<UnexpectedErrorResponse> responseFactory) : base(sender, logger, responseFactory)
        {
        }

        [Function(nameof(GetTutorPersonalInformationQuery))]
        public async Task<IActionResult> GetTutorPersonalInformationQuery([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<GetTutorPersonalInformationQuery>(req, cancellationToken);
        }

        [Function(nameof(UpdateTutorPersonalInformationCommand))]
        public async Task<IActionResult> UpdateTutorPersonalInformationCommand([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<UpdateTutorPersonalInformationCommand>(req, cancellationToken);
        }

        [Function(nameof(GetTutorTeachingProfileQuery))]
        public async Task<IActionResult> GetTutorTeachingProfileQuery([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<GetTutorTeachingProfileQuery>(req, cancellationToken);
        }

        [Function(nameof(UpdateTutorTeachingProfileCommand))]
        public async Task<IActionResult> UpdateTutorTeachingProfileCommand([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<UpdateTutorTeachingProfileCommand>(req, cancellationToken);
        }
    }
}
