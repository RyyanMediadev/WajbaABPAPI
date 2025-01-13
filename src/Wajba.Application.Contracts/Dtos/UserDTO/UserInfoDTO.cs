

using Volo.Abp.Identity;

namespace Wajba.Dtos.UserDTO
{
    public class UserInfoDTO /*:IdentityUserDto*/
    {

        //[MinLength(1), MaxLength(50)]
        //[Required]
        //public string FirstName { get; set; }
        //[MinLength(1), MaxLength(50)]
        //[Required]
        //public string SecoundName { get; set; }
        //[Required]
        //[MinLength(1), MaxLength(50)]

        //public string Phone { get; set; }
        //[Required]
        //public string Email { get; set; }
        //[Required]
        //public string Password { get; set; }
        //[Required]

        //public string Address { get; set; }
        //public int? ProfileId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        //Name = input.Name,
        //Email = input.Email,
        //Password = HashPassword(input.Password)
        public int BranchId { get; set; }


    }
}
