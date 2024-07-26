using API.Models;

namespace API.Interfaces
{
    public interface IMailService
    {
       public Task SendEmailAsyn (MailRequest mailRequest);
    }
}
