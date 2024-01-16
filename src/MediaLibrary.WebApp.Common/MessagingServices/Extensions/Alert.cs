using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaLibrary.WebApp.Common.DialogTemplates;
using MudBlazor;

namespace MediaLibrary.WebApp.Common.MessagingServices
{
    public partial class MudMessagingService
    {
        public void ShowAlert(string title, string message, string acceptButtonText, string icon = Icons.Material.Filled.Info, Color color = Color.Success)
        {
            var parameters = new DialogParameters<SimpleAlertDialogTemplate>();
            parameters.Add(x => x.Icon, icon);
            parameters.Add(x => x.TitleText, title);
            parameters.Add(x => x.MessageText, message);
            parameters.Add(x => x.ButtonText, acceptButtonText);
            parameters.Add(x => x.Color, color);

            _dialogService.Show<SimpleAlertDialogTemplate>(title, parameters);
        }

        public async Task<bool> ShowAlert(string title, string message, string acceptButtonText, string cancelButtonText)
        {
            bool? result = await _dialogService.ShowMessageBox(title, message, acceptButtonText, cancelButtonText);

            return result.HasValue ? result.Value : false;
        }

        public void ShowAlertError(string title, string message, string acceptButtonText)
        {
            ShowAlert(title, message, acceptButtonText, Icons.Material.Filled.ReportProblem, Color.Error);
        }
    }
}
