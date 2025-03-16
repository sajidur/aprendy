using Microsoft.AspNetCore.Components;
using Radzen;

namespace Apprendi.Web.Client.Services
{
    public interface IDialogService
    {
        void Close(dynamic result = null);
        Task<dynamic> OpenAsync<T>(string title, Dictionary<string, object> parameters = null, DialogOptions options = null) where T : ComponentBase;
        Task DisplayErrorMessageWithRefreshAsync(string errorMessage);
        Task DisplayMessage(string message, string title);
        Task DisplayError(string errorMessage);
    }

    public class DialogService : IDialogService
    {
        private readonly Radzen.DialogService _dialogService;
        private readonly NavigationManager _navigationManager;

        public DialogService(Radzen.DialogService dialogService, NavigationManager navigationManager)
        {
            _dialogService = dialogService;
            _navigationManager = navigationManager;
        }

        public void Close(dynamic result = null)
        {
            _dialogService.Close(result);
        }

        public Task<dynamic> OpenAsync<T>(string title, Dictionary<string, object> parameters = null, DialogOptions options = null) where T : ComponentBase
        {
            return _dialogService.OpenAsync<T>(title, parameters, options);
        }

        private async Task BusyDialog(string message)
        {
            await _dialogService.OpenAsync("", ds =>
            {
                RenderFragment content = b =>
                {
                    b.OpenElement(0, "RadzenRow");

                    b.OpenElement(1, "RadzenColumn");
                    b.AddAttribute(2, "Size", "12");

                    b.AddContent(3, message);

                    b.CloseElement();
                    b.CloseElement();
                };
                return content;
            }, new DialogOptions() { ShowTitle = false, Style = "min-height:auto;min-width:auto;width:auto", CloseDialogOnEsc = false });
        }

        public async Task DisplayError(string errorMessage)
        {
            var options = new AlertOptions()
            {
                CloseDialogOnEsc = true,
                ShowClose = true,
                CloseDialogOnOverlayClick = true,
                ShowTitle = true,
                OkButtonText = "Ok"
            };

            await _dialogService.Alert(errorMessage, "Error", options);
        }

        public async Task DisplayMessage(string message, string title)
        {
            var options = new AlertOptions()
            {
                CloseDialogOnEsc = true,
                ShowClose = true,
                CloseDialogOnOverlayClick = true,
                ShowTitle = true,
                OkButtonText = "Ok"
            };

            await _dialogService.Alert(message, title, options);
        }

        public async Task DisplayErrorMessageWithRefreshAsync(string errorMessage)
        {
            var options = new AlertOptions()
            {
                CloseDialogOnEsc = false,
                ShowClose = false,
                CloseDialogOnOverlayClick = false,
                ShowTitle = true, 
                OkButtonText = "Refresh"
            };

            await _dialogService.Alert(errorMessage, "Error", options);

            _navigationManager.NavigateTo(_navigationManager.Uri, forceLoad: true);

            await BusyDialog("Loading...");
        }
    }
}
