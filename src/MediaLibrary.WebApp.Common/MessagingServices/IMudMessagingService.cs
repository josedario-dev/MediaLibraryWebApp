using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binapps.Common.WebApi.Client.Exceptions;
using MudBlazor;

namespace MediaLibrary.WebApp.Common.MessagingServices
{
    public interface IMudMessagingService
    {
        // Dialogs
        Task<DialogResult> ShowCustomDialog(string dialogRef, string title, Dictionary<string, object> parameters = default, MaxWidth maxWidth = MaxWidth.ExtraLarge, Color color = Color.Success);

        // Alert
        void ShowAlert(string title, string message, string acceptButtonText, string icon = Icons.Material.Filled.Info, Color color = Color.Success);
        Task<bool> ShowAlert(string title, string message, string acceptButtonText, string cancelButtonText);
        void ShowAlertError(string title, string message, string acceptButtonText);

        // Error
        void ShowErrorWhitDetails(string acceptButtonText, ServiceException ex);

        // Loader
        void ShowLoader(string text);
        void HideLoader();

        // Snackbar
        void ShowSnackbar(string message, Severity severity = Severity.Normal, int duration = 4000, string position = Defaults.Classes.Position.BottomRight);
    }
}
