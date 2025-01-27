
namespace Wajba.Dtos.WajbaUsersContract;

public class CreateUserDto
{
    public string FullName { get; set; }
    public string Email { get; set; }

    public string Phone { get; set; }

    public int status { get; set; }
    public int Type { get; set; }
    public string? ProfilePhoto { get; set; }
    public int Points { get; set; } = 0;
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public int? Role { get; set; }


    public List<int?> BranchList { get; set; } = new List<int?>();

    //public List<int?> CustomerRoleList { get; set; } = new List<int?>();

    //public static implicit operator CreateUserDto(Wajba.Models.WajbaUserDomain.WajbaUser v)
    //{
    //    throw new NotImplementedException();
    //}
}
