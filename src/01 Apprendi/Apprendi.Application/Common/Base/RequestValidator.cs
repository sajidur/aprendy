using FluentValidation;

namespace Apprendi.Application.Common.Base
{
    public abstract class RequestValidator<TRequest> : AbstractValidator<TRequest>
    { 
    }
}
