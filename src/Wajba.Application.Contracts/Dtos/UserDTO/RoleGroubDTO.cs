using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.UserDTO
{
    public class RoleGroubDTO
    {
        [Required]

        public int RoleId { get; set; }
        [Required]

        public int GroubId { get; set; }

    }
}
