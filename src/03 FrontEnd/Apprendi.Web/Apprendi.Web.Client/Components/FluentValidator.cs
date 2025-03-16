using Apprendi.Application.Common.Base;
using Apprendi.Application.Factories;
using Apprendi.Web.Client.Services.ApiRequestClient;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Apprendi.Web.Client.Components
{
    public sealed class FluentValidator : ComponentBase, IDisposable
    {
        [Inject]
        private IServiceProvider ServiceProvider { get; set; }

        [Inject]
        private IValidationContextFactory ValidationContextFactory { get; set; }

        [Inject]
        private IApiRequestClient ApiRequestClient { get; set; }

        [CascadingParameter]
        private EditContext EditContext { get; set; }

        private ValidationMessageStore _validationMessageStore;
        private EditContext _previousEditContext;
        private static readonly Regex _indexedProperty = new(@"(\w+)\[(\d+)]$", RegexOptions.Compiled); //E.g Items[0]

        protected override void OnInitialized()
        {
            ApiRequestClient.ResponseReceived += OnApiRequestClientResponseReceived;
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            if (EditContext == null)
            {
                throw new NullReferenceException($"{nameof(FluentValidator)} must be placed within an {nameof(EditForm)}");
            }
            
            if (EditContext != _previousEditContext)
            {
                EditContextChanged();
            }

            _previousEditContext = EditContext;
        }

        private IValidator GetValidator(object model)
        {
            var modelType = model.GetType();
            var validatorType = typeof(IValidator<>).MakeGenericType(modelType);
            if (ServiceProvider.GetService(validatorType) is IValidator validator)
            {
                return validator;
            }
            throw new InvalidOperationException($"Unable to find validator service: {modelType.Name}");
        }

        private void UnSubscribeFromEvents(EditContext editContext)
        {
            if (editContext != null)
            {
                editContext.OnValidationRequested -= ValidationRequested;
                editContext.OnFieldChanged -= FieldChanged;
            }
        }

        private void SubscribeToEvents(EditContext editContext)
        {
            editContext.OnValidationRequested += ValidationRequested;
            editContext.OnFieldChanged += FieldChanged;
        }

        private void EditContextChanged()
        {
            _validationMessageStore = new ValidationMessageStore(EditContext);
            UnSubscribeFromEvents(_previousEditContext);
            SubscribeToEvents(EditContext);
        }

        private void ValidationRequested(object sender, ValidationRequestedEventArgs args)
        {
            _validationMessageStore.Clear();

            var validator = GetValidator(EditContext.Model);
            var validationContext = ValidationContextFactory.Create(EditContext.Model);
            var validationResult = validator.Validate(validationContext);

            AddValidationResult(EditContext.Model, validationResult.Errors);
        }
        
        private void FieldChanged(object sender, FieldChangedEventArgs args)
        {
            var fieldIdentifier = args.FieldIdentifier;
            _validationMessageStore.Clear(fieldIdentifier);

            var model = fieldIdentifier.Model;
            var validator = GetValidator(model);
            var validationContext = ValidationContextFactory.Create(model, args.FieldIdentifier.FieldName);
            
            var validationResult = validator.Validate(validationContext);

            AddValidationResult(fieldIdentifier.Model, validationResult.Errors);
        }

        private void OnApiRequestClientResponseReceived(IRequest<Response> request, Response response)
        {
            if (!Equals(request, EditContext.Model)) return;

            _validationMessageStore.Clear();

            if (response.ValidationErrors.Count != 0)
            {
                var validationFailures = new List<ValidationFailure>();  

                foreach (var error in response.ValidationErrors)
                {
                    var validationFailure = new ValidationFailure()
                    {
                        PropertyName = error.PropertyName,
                        ErrorMessage = error.ErrorMessage
                    };
                    validationFailures.Add(validationFailure);
                }

                var validationResult = new ValidationResult(validationFailures);

                AddValidationResult(EditContext.Model, validationResult.Errors);
            }
        }

        private void AddValidationResult(object model, IEnumerable<ValidationFailure> validationFailures)
        {
            var firstErrorForEachProperty = validationFailures.GroupBy(x => x.PropertyName).Select(x => x.First());
            foreach (var error in firstErrorForEachProperty)
            {
                var fieldIdentifier = ResolveFieldIdentifier(model, error.PropertyName);
                _validationMessageStore.Add(fieldIdentifier, error.ErrorMessage);
            }
            EditContext.NotifyValidationStateChanged();
        }

        private static FieldIdentifier ResolveFieldIdentifier(object model, string propertyPath)
        {
            //This method resolves a property path (e.g., User.Addresses[0].Street), navigating objects and collections,
            //to return a FieldIdentifier with the target object (Address) and property (Street)

            object currentObject = model;

            var segments = propertyPath.Split('.');
            for (int i = 0; i < segments.Length - 1; i++)
            {
                var current = segments[i];
                var match = _indexedProperty.Match(current);

                if (match.Success)
                {
                    // Handle indexed properties like Lists or Arrays
                    var propertyName = match.Groups[1].Value;
                    var index = int.Parse(match.Groups[2].Value);

                    var property = GetProperty(currentObject, propertyName);
                    
                    if (property.GetValue(currentObject) is System.Collections.IList list)
                    {
                        currentObject = list[index];
                    }
                    else
                    {
                        throw new InvalidOperationException($"The property '{property.Name}' is not IList");
                    }
                }
                else
                {
                    // Handle regular properties
                    var property = GetProperty(currentObject, current);
                    currentObject = property.GetValue(currentObject);
                }
            }
            
            // The last item is the property name
            var finalPropertyName = segments[segments.Length - 1];
            return new FieldIdentifier(currentObject, finalPropertyName);
        }

        private static PropertyInfo GetProperty(object value, string propertyName)
        {
            var type = value.GetType();
            try
            {
                return type.GetProperty(propertyName) ?? throw new InvalidOperationException($"Property '{propertyName}' not found on type '{type.Name}'.");
            }
            catch (AmbiguousMatchException)
            {
                // Handle cases where 'new' is used on a property. 
                return type.GetProperty(propertyName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                       ?? throw new InvalidOperationException($"Property '{propertyName}' not found on type '{type.Name}'.");
            }
        }

        public void Dispose()
        {
            ApiRequestClient.ResponseReceived -= OnApiRequestClientResponseReceived;
            UnSubscribeFromEvents(EditContext);
        }
    }
}
