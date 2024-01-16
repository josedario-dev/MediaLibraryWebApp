using MediaLibrary.WebApp.Shared.Responses;

namespace MediaLibrary.WebApp.Server.Helpers.Contracts
{
    public interface IMailHelper
    {
        Response SendMail(string toName, string toEmail, string subject, string body);
    }
}
