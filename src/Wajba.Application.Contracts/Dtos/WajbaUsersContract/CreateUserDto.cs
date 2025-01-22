
namespace Wajba.Dtos.WajbaUsersContract;

public class CreateUserDto
{
    public string? FullName { get; set; }
    public string Email { get; set; }

    public string Phone { get; set; }

    public Status status { get; set; }
    public UserTypes Type { get; set; }
    public string? ProfilePhoto { get; set; }
    public int Points { get; set; } = 0;
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    //public static implicit operator CreateUserDto(Wajba.Models.WajbaUserDomain.WajbaUser v)
    //{
    //    throw new NotImplementedException();
    //}
}
