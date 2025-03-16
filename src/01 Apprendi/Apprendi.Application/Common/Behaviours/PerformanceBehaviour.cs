using Apprendi.Application.Factories;
using Apprendi.Application.Common.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Apprendi.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<TRequest> _logger;
        private readonly ITimerFactory _timerFactory;
        private readonly ICurrentUserService _currentUserService;
        private readonly IOptions<PerformanceBehaviourOptions> _options;

        public PerformanceBehaviour(ILogger<TRequest> logger, ITimerFactory timerFactory, ICurrentUserService currentUserService, IOptions<PerformanceBehaviourOptions> options)
        {            
            _logger = logger;
            _timerFactory = timerFactory;
            _currentUserService = currentUserService;
            _options = options;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var timer = _timerFactory.Create();

            try
            {
                timer.Start();

                var response = await next();

                return response;
            }
            finally
            {
                timer.Stop();

                if (timer.Elapsed > _options.Value.LogIfLongerThan)
                {
                    var requestName = typeof(TRequest).Name;
                    var user = _currentUserService.User ?? string.Empty;

                    _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId}",
                        requestName, timer.Elapsed.TotalMilliseconds, user);
                }
            }
            
        }
    }

    public class PerformanceBehaviourOptions
    {
        public TimeSpan LogIfLongerThan { get; set; }
    }
}
