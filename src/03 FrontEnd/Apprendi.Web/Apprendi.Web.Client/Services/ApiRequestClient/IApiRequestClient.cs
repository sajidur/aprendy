using Apprendi.Application.Common;
using Apprendi.Application.Common.Base;
using Apprendi.Application.Factories;
using BitzArt.Blazor.Auth;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Apprendi.Web.Client.Services.ApiRequestClient
{
    public interface IApiRequestClient
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default);
        Task<LogoutResponse> Logout(CancellationToken cancellationToken = default);
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) where TResponse : Response;

        event Action<IRequest<Response>, Response> ResponseReceived;
    }

    public class ApiRequestClient : IApiRequestClient
    {
        private readonly ISender _sender;
        private readonly IResponseFactory<LoginResponse> _loginResponseFactory;
        private readonly IResponseFactory<LogoutResponse> _logoutResponseFactory;
        private readonly IUserService<LoginRequest> _userService;
        private readonly IValidator<LoginRequest> _loginRequestValidator;
        private readonly ILogger<ApiRequestClient> _logger;

        public ApiRequestClient(ISender sender, 
                                HttpClient httpClient, 
                                NavigationManager navigationManager, 
                                IResponseFactory<LoginResponse> loginResponseFactory,
                                IResponseFactory<LogoutResponse> logoutResponseFactory,
                                IUserService<LoginRequest> userService,
                                IValidator<LoginRequest> loginRequestValidator,
                                ILogger<ApiRequestClient> logger)
        {
            _sender = sender;
            _loginResponseFactory = loginResponseFactory;
            _logoutResponseFactory = logoutResponseFactory;
            _userService = userService;
            _loginRequestValidator = loginRequestValidator;
            _logger = logger;
        }
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) where TResponse : Response
        {
            var response = await _sender.Send(request, cancellationToken);
            ResponseReceived?.Invoke(request, response);
            return response;
        }

        public async Task<LogoutResponse> Logout(CancellationToken cancellationToken = default)
        {
            try
            {
                await _userService.SignOutAsync(cancellationToken);
                return _logoutResponseFactory.Success();
            }
            catch (Exception e)
            {
                return _logoutResponseFactory.UnexpectedError(e, _logger);
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var validationResult = _loginRequestValidator.Validate(loginRequest);

                if (validationResult.Errors.Any())
                {
                    var validationErrors = validationResult.Errors
                                 .Select(x => new ValidationError
                                 {
                                     PropertyName = x.PropertyName,
                                     ErrorMessage = x.ErrorMessage
                                 });
                    return _loginResponseFactory.BadRequest(validationErrors);
                }
                else
                {
                    var result = await _userService.SignInAsync(loginRequest);

                    if (result.IsSuccess)
                    {
                        return _loginResponseFactory.Success();
                    }
                    else
                    {
                        return _loginResponseFactory.BadRequest(result.ErrorMessage);
                    }
                }   
            }
            catch (Exception e)
            {
                return _loginResponseFactory.UnexpectedError(e, _logger);
            }
        }

        public event Action<IRequest<Response>, Response> ResponseReceived;
    }
}
