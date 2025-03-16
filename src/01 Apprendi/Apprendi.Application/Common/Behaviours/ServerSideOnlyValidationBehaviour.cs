using Apprendi.Application.Common.Base;
using Apprendi.Application.Factories;
using FluentValidation;
using MediatR;

namespace Apprendi.Application.Common.Behaviours
{
    public class ServerSideOnlyValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : Request<TResponse>
         where TResponse : Response, new()
    {        
        private readonly IEnumerable<ServerOnlyRequestValidator<TRequest, TResponse>> _serverOnlyRequestValidators;

        public ServerSideOnlyValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _serverOnlyRequestValidators = validators.OfType<ServerOnlyRequestValidator<TRequest, TResponse>>();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            foreach (var validator in _serverOnlyRequestValidators)
            {
                var response = await validator.Handle(request, cancellationToken);

                if (!response.IsSuccess)
                {
                    return response;
                }
            }

            return await next();
        }
    }
}
