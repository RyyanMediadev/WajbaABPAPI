﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.WajbaUsersContract
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int? Role { get; set; }
        public int? GenderType { get; set; }
        public string? ProfilePhoto { get; set; }




    }
}
