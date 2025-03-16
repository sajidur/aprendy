using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Apprendi.Application.Factories
{
    public interface IValidationContextFactory
    {
        IValidationContext Create<TRequest>(TRequest request);
        IValidationContext Create(object request);
        IValidationContext Create(object request, params string[] properties);
    }

    public class ValidationContextFactory : IValidationContextFactory
    {
        public IValidationContext Create<TRequest>(TRequest request)
        {
            return new ValidationContext<TRequest>(request);
        }

        public IValidationContext Create(object request, params string[] properties)
        {
            return ValidationContext<object>.CreateWithOptions(request, strategy =>
            {
                strategy.IncludeProperties(properties);
            });
        }

        public IValidationContext Create(object request)
        {            
            var validationContextType = typeof(ValidationContext<>).MakeGenericType(request.GetType());
            return (IValidationContext)Activator.CreateInstance(validationContextType, [request]);
        }
    }
}
