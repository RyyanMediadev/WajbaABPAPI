namespace Wajba.Dtos.CustomerContract;

public class CreateUserDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int status { get; set; }
    public int Type { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

}
