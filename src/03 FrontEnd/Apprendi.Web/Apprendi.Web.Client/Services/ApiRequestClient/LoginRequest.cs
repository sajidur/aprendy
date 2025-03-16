using Apprendi.Application.Common.Base;

namespace Apprendi.Web.Client.Services.ApiRequestClient
{
    public class LoginRequest : Request<LoginResponse> 
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
