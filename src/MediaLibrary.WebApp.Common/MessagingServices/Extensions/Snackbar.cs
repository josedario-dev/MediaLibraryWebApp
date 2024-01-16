using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MudBlazor;
using static MudBlazor.Defaults.Classes;

namespace MediaLibrary.WebApp.Common.MessagingServices
{
    public partial class MudMessagingService
    {
        public void ShowSnackbar(string message, Severity severity = Severity.Normal, int duration = 4000, string position = Defaults.Classes.Position.BottomRight)
        {
            _snackbarService.Clear();
            _snackbarService.Configuration.PositionClass = position;
            _snackbarService.Add(message, severity, config => 
                {
                    config.ShowCloseIcon = true;
                    config.VisibleStateDuration = duration;
                    config.HideTransitionDuration = 500;
                    config.ShowTransitionDuration = 500;
                    config.SnackbarVariant = Variant.Filled;
                });
        }
    }
}
