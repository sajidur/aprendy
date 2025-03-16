using MediatR;

namespace Apprendi.Application.Common.Base
{
    public abstract class Request<TResponse> : IRequest<TResponse> where TResponse : Response, new()
    {
    }
}
