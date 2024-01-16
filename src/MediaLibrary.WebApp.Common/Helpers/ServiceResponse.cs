using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Common.Helpers
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success => string.IsNullOrEmpty(ErrorMessage);

        public ServiceResponse(T? data, string errorMessage)
        {
            Data = data;
            ErrorMessage = errorMessage;
        }
    }
}
