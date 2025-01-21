using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.UserManagment
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string HostRead { get; set; }
        public int PortRead { get; set; }
        public string SMSUser { get; set; }
        public string SMSPass { get; set; }

        public string SMSAPI { get; set; }
        public string ProfileId { get; set; }
        public string SenderId { get; set; }
        public string Pass { get; set; }
        public string VAT { get; set; }

    }
}
