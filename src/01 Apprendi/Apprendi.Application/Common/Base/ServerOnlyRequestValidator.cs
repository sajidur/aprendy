using Apprendi.Application.Factories;

namespace Apprendi.Application.Common.Base
{
    public abstract class ServerOnlyRequestValidator<TRequest, TResponse> : RequestValidator<TRequest>
        where TRequest : Request<TResponse>
        where TResponse : Response, new()
    {
        public ServerOnlyRequestValidator(IResponseFactory<TResponse> responseFactory)
        {   
            Response = responseFactory;
        }

        public IResponseFactory<TResponse> Response { get; }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
