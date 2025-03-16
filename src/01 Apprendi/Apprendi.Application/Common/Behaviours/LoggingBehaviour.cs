using Apprendi.Application.Common.Services;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Apprendi.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var user = _currentUserService.User ?? string.Empty;

            _logger.LogInformation("Request: {RequestName} {@UserId}", requestName, user);

            return Task.CompletedTask;
        }
    }
}
