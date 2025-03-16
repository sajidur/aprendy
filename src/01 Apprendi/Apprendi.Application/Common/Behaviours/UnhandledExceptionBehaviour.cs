using Apprendi.Application.Common.Base;
using Apprendi.Application.Factories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Apprendi.Application.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>        
        where TResponse : Response, new()
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IResponseFactory<TResponse> _responseFactory;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger, IResponseFactory<TResponse> responseFactory)
        {
            _logger = logger;
            _responseFactory = responseFactory;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogError(ex, "Unhandled Exception for Request {Name}", requestName);

                return _responseFactory.UnexpectedError(ex, _logger);
            }
        }
    }
}
