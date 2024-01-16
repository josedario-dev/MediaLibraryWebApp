using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaLibrary.WebApp.Common.DialogTemplates;
using MudBlazor;

namespace MediaLibrary.WebApp.Common.MessagingServices
{
    public partial class MudMessagingService : IMudMessagingService
    {
        private readonly IDialogService _dialogService;
        private readonly ISnackbar _snackbarService;

        public MudMessagingService(IDialogService dialogService, ISnackbar snackbarService)
        {
            _dialogService = dialogService;
            _snackbarService = snackbarService;
        }

        public async Task<DialogResult> ShowCustomDialog(string dialogRef, string title, Dictionary<string, object> parameters = default, MaxWidth maxWidth = MaxWidth.ExtraLarge, Color color = Color.Dark)
        {
            Type? dialogType = Type.GetType(dialogRef);
            var baseParameters = new DialogParameters<CustomDialogTemplate>();
            //baseParameters.Add(x => x.Icon, icon);
            baseParameters.Add(x => x.TitleText, title);
            baseParameters.Add(x => x.DialogName, dialogType);
            //baseParameters.Add(x => x.CancelButtonText, cancelButtonText);
            //baseParameters.Add(x => x.AcceptButtonText, acceptButtonText);
            baseParameters.Add(x => x.Color, color);
            baseParameters.Add(x => x.AditionalParameters, parameters);

            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = maxWidth, FullWidth = true, DisableBackdropClick = true };

            return await _dialogService.Show<CustomDialogTemplate>(title, baseParameters, options).Result;
        }
    }
}
