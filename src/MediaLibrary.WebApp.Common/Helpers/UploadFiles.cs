using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaLibrary.WebApp.Common.MessagingServices;
using MediaLibrary.WebApp.Common.ConstantsGlobals;
using MediaLibrary.WebApp.Common.Helpers.Contracts;

namespace MediaLibrary.WebApp.Common.Helpers
{
    public class UploadFiles : IUploadFiles
    {
        private IMudMessagingService _mudMessagingService;
        public UploadFiles()
        {

        }

        private string errorMessage;
        private IBrowserFile fileReturn;

        public IBrowserFile UploadFile(IBrowserFile file, string fileTypes)
        {
            errorMessage = null; // Limpiar el mensaje de error anterior

            // Validar tamaño del archivo
            if (file.Size > Constants.MaxFileSize)
            {
                errorMessage = $"El archivo es demasiado grande. El tamaño máximo permitido es de 20MB. {file.Name}";
                _mudMessagingService.ShowAlertError("Error", $"El archivo es demasiado grande. El tamaño máximo permitido es de 20MB. {file.Name}", "Aceptar");
                return null;
            };

            // Validar extensión del archivo
            var extension = Path.GetExtension(file.Name).ToLower();
            if (fileTypes == null || fileTypes == "*.*" || fileTypes == ".*" || fileTypes == "." || fileTypes == "*.")
            {}
            else
            {
                if (!fileTypes.Split(",").Contains(extension))
                {
                    errorMessage = "Tipo de archivo no permitido.";
                    _mudMessagingService.ShowAlertError("Error", "Tipo de archivo no permitido", "Aceptar");
                    return null;
                }
            }
            return file;
        }
    }
}
