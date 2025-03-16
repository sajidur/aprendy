using BitzArt.Blazor.Auth;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Apprendi.Web.Services
{
    public interface IJwtService
    {
        JwtPair BuildJwtPair(IEnumerable<Claim> accssTokenClaims, IEnumerable<Claim> refreshTokenClaims, DateTimeOffset expiresOn);
        bool TryReadClaims(string token, out Dictionary<string, string> claims);
    }

    public class JwtServiceOptions
    {
        public string Secret { get; set; }
    }

    public class JwtService : IJwtService
    {
        private readonly SymmetricSecurityKey _securityKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public JwtService(IOptions<JwtServiceOptions> options)
        {
            if (string.IsNullOrEmpty(options.Value?.Secret) || options.Value?.Secret?.Length < 32)
            {
                throw new ArgumentException("JWT Secret Key must be at least 32 characters long.", nameof(options.Value.Secret));
            }

            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Secret));
            _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public JwtPair BuildJwtPair(IEnumerable<Claim> accssTokenClaims, IEnumerable<Claim> refreshTokenClaims, DateTimeOffset expiresOn)
        {
            var now = DateTime.UtcNow;
            
            var accessTokenExpiresAt = expiresOn.UtcDateTime;
            var refreshTokenExpiresAt = expiresOn.UtcDateTime;

            var token = new JwtSecurityToken(claims: accssTokenClaims,
                                            notBefore: now,
                                            expires: accessTokenExpiresAt,
                                            signingCredentials: _signingCredentials);

            var accessToken = _tokenHandler.WriteToken(token);

            
            token = new JwtSecurityToken(claims: refreshTokenClaims,
                                         notBefore: now,
                                         expires: refreshTokenExpiresAt,
                                         signingCredentials: _signingCredentials);

            var refreshToken = _tokenHandler.WriteToken(token);

            return new JwtPair(accessToken, accessTokenExpiresAt, refreshToken, refreshTokenExpiresAt);
        }

        public bool TryReadClaims(string token, out Dictionary<string, string> claims)
        {
            if (ValidateToken(token))
            {
                var jwtToken = _tokenHandler.ReadJwtToken(token);
                claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);
                return true;
            }
            else
            {
                claims = [];
                return false;
            }   
        }

        private bool ValidateToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, 
                ValidateAudience = false,
                ValidateLifetime = true, 
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _securityKey,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                _tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return true; 
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
