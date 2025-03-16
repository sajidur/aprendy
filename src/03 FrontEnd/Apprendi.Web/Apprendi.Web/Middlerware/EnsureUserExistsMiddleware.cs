using Apprendi.Application.Features.Users.Commands.AddUser;
using Apprendi.Application.Features.Users.Queries.GetUserByEmail;
using Apprendi.Web.Client.Services;
using MediatR;
using System.Security.Claims;

namespace Apprendi.Web.Middlerware
{
    public class EnsureUserExistsMiddleware
    {
        private readonly RequestDelegate _next;

        public EnsureUserExistsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISender sender)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                if (context.User.Identity is ClaimsIdentity claimsIdentity)
                {
                    var email = context.User.Identity.Name;

                    var request = new GetUserByEmailQuery
                    {
                        Email = email,
                    };

                    var response = await sender.Send(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound) 
                    {
                        var name = claimsIdentity.FindFirst("name");

                        var addUserRequest = new AddUserCommand
                        {
                            Email = email,
                            FirstName = name.Value,
                        };

                        var addUserResponse =  await sender.Send(addUserRequest);
                    }
                }
                else
                {
                    //todo:
                }
            }
            
            await _next(context);
        }
    }
}
