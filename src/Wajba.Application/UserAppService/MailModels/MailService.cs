

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Tls;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace Wajba.UserAppService
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, MimeKit.ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        public async Task<string>  SendWelcomeEmailAsync(WelcomeRequest request)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + @"\wwwroot\EmailTemps\ActivateTemp.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("{UserName}", request.UserName).Replace("{Link}", request.Link).Replace("[ID]",request.Id.ToString());
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(request.ToEmail));
                email.Subject = $"Welcome {request.UserName}";
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                var smtp = new MailKit.Net.Smtp.SmtpClient();

                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return "OK";

            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
            
        }


        public async Task<string> SendActivateEmailAsync(WelcomeRequest request)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + @"\wwwroot\EmailTemps\ActivateTempEmail.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("{UserName}", request.UserName).Replace("{Link}", request.Link).Replace("[ID]", request.Id.ToString());
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(request.ToEmail));
                email.Subject = $"Welcome {request.UserName}";
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return "OK";

            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }

        public async Task<string> Notifacation(string to , string Contect ,string subject)
        {
            try
            {
               // string FilePath = Directory.GetCurrentDirectory() + @"\wwwroot\EmailTemps\ActivateTempEmail.html";
              //  StreamReader str = new StreamReader(FilePath);
                // string MailText = str.ReadToEnd();
                string MailText = Contect;
               // str.Close();
               // MailText = MailText.Replace("{UserName}", request.UserName).Replace("{Link}", request.Link).Replace("[ID]", request.Id.ToString());
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return "OK";

            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }
    }
}
