using Apprendi.Application.Common.Base;
using Apprendi.Application.Factories;
using Apprendi.Function.Endpoints;
using Grpc.Net.Client.Balancer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Apprendi.Function.Base
{
    public class UnexpectedErrorResponse : Response { };

    public class BaseFunction
    {
        private readonly ISender _sender;
        private readonly ILogger<UserFunction> _logger;
        private readonly IResponseFactory<UnexpectedErrorResponse> _responseFactory;
        private readonly JsonSerializerOptions _serializerOptions;

        public BaseFunction(ISender sender, ILogger<UserFunction> logger, IResponseFactory<UnexpectedErrorResponse> responseFactory)
        {
            _sender = sender;
            _logger = logger;
            _responseFactory = responseFactory;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        protected async Task<IActionResult> Result<TRequest>(HttpRequest httpRequest, CancellationToken cancellationToken)
            where TRequest : IBaseRequest
        {
            try
            {
                var request = await JsonSerializer.DeserializeAsync<TRequest>(httpRequest.Body, _serializerOptions);
                return await Result(request, cancellationToken);
            }
            catch (Exception ex)
            {
                var response = _responseFactory.UnexpectedError(ex, _logger);
                return GetResult(response);
            }
        }

        protected async Task<IActionResult> Result<TRequest>(TRequest request, CancellationToken cancellationToken) 
            where TRequest : IBaseRequest
        {
            try
            {                
                var response = (Response)await _sender.Send(request, cancellationToken);
                return GetResult(response);
            }
            catch (Exception ex)
            {
                var response = _responseFactory.UnexpectedError(ex, _logger);
                return GetResult(response);
            }
        }

        private static ActionResult GetResult(Response response)
        {
            return new ObjectResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }
    }
}
