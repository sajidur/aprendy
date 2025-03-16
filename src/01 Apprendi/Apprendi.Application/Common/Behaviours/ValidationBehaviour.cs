using Apprendi.Application.Common.Base;
using Apprendi.Application.Factories;
using FluentValidation;
using MediatR;

namespace Apprendi.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : Request<TResponse>
         where TResponse : Response, new()
    {        
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IValidationContextFactory _validationContextFactory;
        private readonly IResponseFactory<TResponse> _responseFactory;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators,
                                   IValidationContextFactory validationContextFactory,
                                   IResponseFactory<TResponse> responseFactory)
        {
            _validators = validators;
            _validationContextFactory = validationContextFactory;
            _responseFactory = responseFactory;            
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = _validationContextFactory.Create(request);

                var validationErrors = new List<ValidationError>();
                

                foreach (var validator in _validators)
                {
                    var validationResult = await validator.ValidateAsync(context, cancellationToken);

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult
                            .Errors
                            .Select(x => new ValidationError
                            {
                                PropertyName = x.PropertyName,
                                ErrorMessage = x.ErrorMessage
                            });

                        validationErrors.AddRange(errors);
                    }
                }

                if (validationErrors.Any())
                {
                    return _responseFactory.BadRequest(validationErrors);
                }
            }

            return await next();
        }
    }
}
