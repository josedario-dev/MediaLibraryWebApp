using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Common.Helpers.Contracts
{
    public interface IUploadFiles
    {
        IBrowserFile UploadFile(IBrowserFile file, string fileTypes);
    }
}
