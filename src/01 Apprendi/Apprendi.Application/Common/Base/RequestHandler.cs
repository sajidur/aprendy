using MediatR;
using Apprendi.Application.Factories;

namespace Apprendi.Application.Common.Base
{
    public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : Request<TResponse>
        where TResponse : Response, new()
    {
        public RequestHandler(IResponseFactory<TResponse> responseFactory)
        {
            Response = responseFactory;
        }

        protected IResponseFactory<TResponse> Response { get; }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
