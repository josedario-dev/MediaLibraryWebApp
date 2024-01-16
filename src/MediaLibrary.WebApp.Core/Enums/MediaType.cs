using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Core.Enums
{
    public enum MediaType
    {
        [Display(Name = "Imagen")]
        Picture,

        [Display(Name = "Video")]
        Video,

        [Display(Name = "Audio")]
        Audio,

        [Display(Name = "Archivo")]
        File
    }
}
