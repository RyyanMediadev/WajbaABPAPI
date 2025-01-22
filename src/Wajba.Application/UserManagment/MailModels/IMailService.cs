


namespace Wajba.UserManagment
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest); 
          Task<string> SendWelcomeEmailAsync(WelcomeRequest request);
          Task<string> SendActivateEmailAsync(WelcomeRequest request);
          Task<string> Notifacation(string to , string Content,string subject);
    }
}
