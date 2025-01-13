namespace Wajba.Models.UsersDomain;

public class Customer:IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Customer()
    {
        
    }
}
