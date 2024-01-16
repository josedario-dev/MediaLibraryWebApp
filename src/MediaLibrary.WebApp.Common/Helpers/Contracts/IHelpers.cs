using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Common.Helpers.Contracts
{
    public interface IHelpers
    {
        string Encrypt(string plainText, string salt = null);
        string? Decrypt(string cipherText, string? salt = null);
    }
}
