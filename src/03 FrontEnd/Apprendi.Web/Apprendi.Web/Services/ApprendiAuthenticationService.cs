using Apprendi.Web.Client.Services.ApiRequestClient;
using BitzArt.Blazor.Auth.Server;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Security.Claims;
using AuthenticationResult = BitzArt.Blazor.Auth.AuthenticationResult;

namespace Apprendi.Web.Services
{
    public class ClaimIdentifiers
    {
        public const string RtIdentitifer = nameof(RtIdentitifer);
        public const string RtLoginHint = nameof(RtLoginHint);
    }

    public class ApprendiAuthenticationService : AuthenticationService<LoginRequest>
    {
        private readonly IJwtService _jwtService;
        private readonly IPublicClientApplication _publicClientApplication;
        private readonly GraphServiceClient _graphServiceClient;
        private readonly ILogger<ApprendiAuthenticationService> _logger;
        private readonly string _issuer;

        public ApprendiAuthenticationService(IJwtService jwtService, 
                                             IConfiguration configuration,
                                             IPublicClientApplication publicClientApplication,
                                             GraphServiceClient graphServiceClient,
                                             ILogger<ApprendiAuthenticationService> logger)
        {
            _jwtService = jwtService;
            _publicClientApplication = publicClientApplication;
            _graphServiceClient = graphServiceClient;
            _logger = logger;
            _issuer = configuration["AzureAd:Issuer"];
        }


        public override async Task<AuthenticationResult> SignInAsync(LoginRequest signInPayload, CancellationToken cancellationToken = default)
        {
            try
            {
                var userPrincipalName = await GetUserPrincipalName(signInPayload.Username);

                if (userPrincipalName != null)
                {
                    var result = await _publicClientApplication
                        .AcquireTokenByUsernamePassword(new string[] { "https://graph.microsoft.com/.default" }, userPrincipalName, signInPayload.Password)
                        .ExecuteAsync(cancellationToken);

                    var identifier = result.Account.HomeAccountId.Identifier;
                    var loginHint = result.Account.Username;

                    var accessTokenClaims = GetAccessTokenClaims(signInPayload.Username);
                    var refreshTokenClaims = GetRefreshTokenClaims(identifier, loginHint, signInPayload.Username);

                    var jwtPair = _jwtService.BuildJwtPair(accessTokenClaims, refreshTokenClaims, result.ExpiresOn);
                    return Success(jwtPair);
                }
                else
                {
                    return AuthenticationResult.Failure("Invalid username or password");
                }
            }
            catch (MsalUiRequiredException e)
            {
                _logger.LogError(e, "Error while authenticating");
                return AuthenticationResult.Failure("Invalid username or password");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while authenticating");
                return AuthenticationResult.Failure("Error while authenticating");
            }
        }

        private async Task<string> GetUserPrincipalName(string emailAddress)
        {
            var result = await _graphServiceClient
                .Users
                .GetAsync(requestConfiguration =>
                {
                    var filter = $"identities/any(i:i/issuer eq '{_issuer}' and i/issuerAssignedId eq '{emailAddress}')";
                    requestConfiguration.QueryParameters.Filter = filter;
                });
            return result.Value.FirstOrDefault().UserPrincipalName;
        }

        private static Claim[] GetRefreshTokenClaims(string identifier, string loginHint, string username)
        {
            return [ new Claim(ClaimTypes.NameIdentifier, username),
                     new Claim(ClaimIdentifiers.RtIdentitifer, identifier),
                     new Claim(ClaimIdentifiers.RtLoginHint, loginHint) ];
        }

        private static Claim[] GetAccessTokenClaims(string username)
        {
            return [
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, username)
            ];
        }

        public override async Task<AuthenticationResult> RefreshJwtPairAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            if (_jwtService.TryReadClaims(refreshToken, out var claims))
            {
                if (claims.TryGetValue(ClaimTypes.NameIdentifier, out var username))
                {
                    if (claims.TryGetValue(ClaimIdentifiers.RtLoginHint, out var loginHint))
                    {
                        try
                        {
                            var result = await _publicClientApplication
                                .AcquireTokenSilent([], loginHint)
                                .ExecuteAsync(cancellationToken);

                            var identifier = result.Account.HomeAccountId.Identifier;

                            var accessTokenClaims = GetAccessTokenClaims(username);
                            var refreshTokenClaims = GetRefreshTokenClaims(identifier, loginHint, username);

                            var jwtPair = _jwtService.BuildJwtPair(accessTokenClaims, refreshTokenClaims, result.ExpiresOn);
                            return AuthenticationResult.Success(jwtPair);
                        }
                        catch (MsalUiRequiredException e)
                        {
                            _logger.LogError(e, "Error while authenticating");
                            return AuthenticationResult.Failure("Invalid username or password");
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e, "Error while authenticating");
                            return AuthenticationResult.Failure("Error while authenticating");
                        }
                    }
                }
            }
            return AuthenticationResult.Failure("Invalid token");
        }
    }
}
