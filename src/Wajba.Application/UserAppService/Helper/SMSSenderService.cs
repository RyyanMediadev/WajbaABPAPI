using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.UserAppService
{
    public interface ISmsSenderService
    {
        public bool SendSMS(string Number, string Message);
    }
    public class SMSSenderService : ISmsSenderService
    {
        private readonly MailSettings _mailSettings;
        public SMSSenderService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public bool SendSMS(string Number, string Message)
        {
          try
            {
                string smsURL = " @جدييد   " + Message + "&mobiles=" + Number;
                System.Net.WebClient webClient = new System.Net.WebClient();
                string result = webClient.DownloadString(smsURL);
            
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
