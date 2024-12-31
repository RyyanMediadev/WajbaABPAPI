

namespace Wajba.Dtos.UserDTO
{
    public class ChangePasswordDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]

        public string NewPassword { get; set; }
        [Required]

        public string OldPassword { get; set; }
    }
}
