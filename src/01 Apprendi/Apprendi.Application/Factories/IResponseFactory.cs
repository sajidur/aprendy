using Apprendi.Application.Common.Base;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Apprendi.Application.Factories
{
    public interface IResponseFactory<TResponse> where TResponse : Response, new()
    {
        TResponse BadRequest(string errorMessage);
        TResponse ConflictFailure(string errorMessage);
        TResponse NotFound(string errorMessage);
        TResponse ServiceUnavailable(string errorMessage);
        TResponse Success();
        TResponse Success(Action<TResponse> response);
        TResponse UnexpectedError(Exception exception, ILogger logger);        
        TResponse BadRequest(IEnumerable<ValidationError> validationFailures);
        TResponse UnexpectedError(string errorMessage);
    }

    public class ResponseFactory<TResponse> : IResponseFactory<TResponse> where TResponse : Response, new()
    {
        private static TResponse CreateResponse(HttpStatusCode statusCode)
        {
            var r = Activator.CreateInstance<TResponse>();
            r.IsSuccess = statusCode == HttpStatusCode.OK;
            r.StatusCode = statusCode;
            return r;
        }

        public TResponse Success()
        {
            return CreateResponse(HttpStatusCode.OK);
        }

        public TResponse Success(Action<TResponse> response)
        {
            var r = CreateResponse(HttpStatusCode.OK);
            response.Invoke(r);
            return r;
        }

        public TResponse NotFound(string errorMessage)
        {
            var r = CreateResponse(HttpStatusCode.NotFound);
            r.ErrorMessage = errorMessage;
            return r;
        }

        public TResponse ConflictFailure(string errorMessage)
        {
            var r = CreateResponse(HttpStatusCode.Conflict);
            r.ErrorMessage = errorMessage;
            return r;
        }

        public TResponse UnexpectedError(Exception exception, ILogger logger)
        {
            var r = CreateResponse(HttpStatusCode.InternalServerError);

            var errorId = $"Error_{Guid.NewGuid():n}";

            r.ErrorMessage = $"An unexpected error has occurred. Please try again.\n" +
                             $"If the problem persists please contact an admin.\nError ID: {errorId}";

            logger.LogError(exception, "{ErrorId} - {EnvironmentStackTrace}", errorId, Environment.StackTrace);

            return r;
        }

        public TResponse BadRequest(IEnumerable<ValidationError> validationErrors)
        {
            var r = CreateResponse(HttpStatusCode.BadRequest);
            r.ValidationErrors = validationErrors.ToList();
            return r;
        }

        public TResponse BadRequest(string errorMessage)
        {
            var r = CreateResponse(HttpStatusCode.BadRequest);
            r.ErrorMessage = errorMessage;
            return r;
        }

        public TResponse UnexpectedError(string errorMessage)
        {
            var r = CreateResponse(HttpStatusCode.InternalServerError);
            r.ErrorMessage = errorMessage;
            return r;
        }

        public TResponse ServiceUnavailable(string errorMessage)
        {
            var r = CreateResponse(HttpStatusCode.ServiceUnavailable);
            r.ErrorMessage = errorMessage;
            return r;
        }
    }
}
