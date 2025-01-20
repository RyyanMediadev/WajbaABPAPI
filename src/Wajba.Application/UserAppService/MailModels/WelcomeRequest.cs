using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.UserAppService
{
    public class WelcomeRequest
    {
        public string ToEmail { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Link { get; set; }
    }
}
