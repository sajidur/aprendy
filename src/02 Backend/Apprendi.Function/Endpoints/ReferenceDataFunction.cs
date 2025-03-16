using Apprendi.Application.Factories;
using Apprendi.Application.Features.ReferenceData.Queries.GetCountries;
using Apprendi.Application.Features.ReferenceData.Queries.GetReferenceData;
using Apprendi.Application.Features.ReferenceData.Queries.GetTimeZones;
using Apprendi.Application.Features.SignUp.Queries;
using Apprendi.Function.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Apprendi.Function.Endpoints
{
    [Route("ReferenceData")]
    public class ReferenceDataFunction : BaseFunction
    {
        private readonly ISender _sender;
        private readonly ILogger<UserFunction> _logger;
        private readonly IResponseFactory<UnexpectedErrorResponse> _responseFactory;

        public ReferenceDataFunction(ISender sender, ILogger<UserFunction> logger, IResponseFactory<UnexpectedErrorResponse> responseFactory) : base(sender, logger, responseFactory)
        {
            _sender = sender;
            _logger = logger;
            _responseFactory = responseFactory;
        }

        [Function(nameof(GetReferenceDataQuery))]
        public async Task<IActionResult> GetReferenceDataQuery([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<GetReferenceDataQuery>(req, cancellationToken);            
        }

        [Function(nameof(GetTimeZonesQuery))]
        public async Task<IActionResult> GetTimeZonesQuery([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<GetTimeZonesQuery>(req, cancellationToken);
        }

        [Function(nameof(GetCountriesQuery))]
        public async Task<IActionResult> GetCountriesQuery([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<GetCountriesQuery>(req, cancellationToken);
        }

        [Function(nameof(GetTutorAboutStageDetailsQuery))]
        public async Task<IActionResult> GetTutorAboutStageDetailsQuery([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req, CancellationToken cancellationToken)
        {
            return await Result<GetTutorAboutStageDetailsQuery>(req, cancellationToken);
        }
    }
}
