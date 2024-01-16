using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binapps.Common.WebApi.Client.Exceptions;
using MediaLibrary.WebApp.Common.DialogTemplates;
using MudBlazor;

namespace MediaLibrary.WebApp.Common.MessagingServices
{
    public partial class MudMessagingService
    {
        public void ShowErrorWhitDetails(string acceptButtonText, ServiceException ex)
        {
            var parameters = new DialogParameters<ErrorWhitDetailsDialogTemplate>();
            parameters.Add(x => x.ContentText, ex.ErrorMessage);
            parameters.Add(x => x.ButtonText, acceptButtonText);
            parameters.Add(x => x.Color, Color.Success);

            _dialogService.Show<SimpleAlertDialogTemplate>("Error", parameters);
        }
    }
}
