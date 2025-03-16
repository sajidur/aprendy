using Apprendi.Application.Features.Users;
using Apprendi.Application.Features.Users.Commands.AddUser;
using Apprendi.Application.Features.Users.Commands.UpdateUser;
using Apprendi.Web.Client.Services.ApiRequestClient;
using MediatR;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Apprendi.Web.Client.Pages
{
    public sealed partial class Home : ComponentBase
    {
        [Inject] private IApiRequestClient ApiRequestClient { get; set; }
        [Inject] private PersistentComponentState ApplicationState { get; set; }

        private PersistingComponentStateSubscription _persistingSubscription;

        private readonly UpdateUserCommand _updateUserCommand = new UpdateUserCommand()
        {
            User = new UserDto
            {
                Email = "a@a.com"
            }
        };

        private bool isSubmitted = false;

        protected override void OnInitialized()
        {
            _persistingSubscription = ApplicationState.RegisterOnPersisting(PersistState);
        }

        private Task PersistState()
        {
            var state = new HomeComponentState
            {
                Command = _updateUserCommand
            };
            ApplicationState.PersistAsJson(nameof(HomeComponentState), state);
            return Task.CompletedTask;
        }

        private void HandleValidSubmit()
        {
            isSubmitted = true;
            Console.WriteLine($"Name: {_updateUserCommand.User.FirstName}, Email: {_updateUserCommand.User.Email}, Age: {_updateUserCommand.User.LastName}");
        }
        private async Task OnClick(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            var response = await ApiRequestClient.Send(_updateUserCommand);
            
            if (response.IsSuccess)
            {

            }
        }

        public void Dispose()
        {
            _persistingSubscription.Dispose();
        }
    }

    internal class HomeComponentState
    {
        public UpdateUserCommand Command { get; set; }
    }
}
