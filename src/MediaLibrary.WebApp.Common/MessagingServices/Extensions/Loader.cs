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
        private IDialogReference? _loaderReference;

        public void ShowLoader(string text)
        {
            var parameters = new DialogParameters<LoaderDialogTemplate>();
            parameters.Add(x => x.ContentText, text);
            parameters.Add(x => x.Color, Color.Success);

            var options = new DialogOptions
            {
                CloseButton = false,
                DisableBackdropClick = true,
                NoHeader = true,
                CloseOnEscapeKey = false
            };

            _loaderReference = _dialogService.Show<LoaderDialogTemplate>(null, parameters, options);
        }

        public void HideLoader()
        {
            if (_loaderReference != null)
            {
                _loaderReference.Close();
                _loaderReference = null;
            }
        }
    }
}
