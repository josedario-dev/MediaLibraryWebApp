using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Common.Helpers
{
    public class PasswordVisibility
    {
        public void TogglePasswordVisibility(ref bool passwordVisibility, out InputType passwordInput, out string passwordIcon)
        {
            passwordVisibility = !passwordVisibility;
            if (passwordVisibility)
            {
                passwordInput = InputType.Text;
                passwordIcon = Icons.Material.Filled.Visibility;
            }
            else
            {
                passwordInput = InputType.Password;
                passwordIcon = Icons.Material.Filled.VisibilityOff;
            }
        }
    }
}
