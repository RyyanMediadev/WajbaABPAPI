using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.Enums;

namespace Wajba.Dtos.CustomerContract
{
    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Status status { get; set; }
        public UserTypes Type { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
