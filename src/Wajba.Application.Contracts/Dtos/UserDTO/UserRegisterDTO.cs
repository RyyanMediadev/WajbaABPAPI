

namespace Wajba.Dtos.UserDTO
{
    public class UserRegisterDTO
    {


        [MinLength(1), MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        [MinLength(1), MaxLength(50)]
        [Required]
        public string SecoundName { get; set; }
        [Required]
        [MinLength(1), MaxLength(50)]

        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]

        public string Address { get; set; }
        public int? ProfileId { get; set; }




    }
}
