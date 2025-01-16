using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.CustomerContract
{
    public class GetUserListDto
    {
        public string? FullName { get; set; }
        public UserTypes? Type { get; set; }
        public Status? Status { get; set; }
        public int MaxResultCount { get; set; } = 10;
        public int SkipCount { get; set; } = 0;
    }
}
