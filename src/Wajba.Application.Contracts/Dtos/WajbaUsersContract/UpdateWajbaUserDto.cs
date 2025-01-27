using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.WajbaUsersContract
{
    public class UpdateWajbaUserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int? Role { get; set; }

        public List<int?> BranchList { get; set; } = new List<int?>();

		//public List<> CustomerRoleList { get; set; } = new List<int?>();
	}
}
