global using Volo.Abp.Identity;

namespace Wajba.Models.UsersDomain;

public class APPUser : IdentityUser//*: FullAuditedEntity<int>*/
{
    public APPUser()
    {

    }

    //[Required]
    //public string FirstName { get; set; }
    //[Required]
    //public string SecoundName { get; set; }
    //[Required]
    //public string Phone { get; set; }

    //[Required]
    //public string Email { get; set; }
    //[Required]
    //public string Password { get; set; }
    //[Required]
    //public string Address { get; set; }
    //public int? ProfileId { get; set; }
    //public Profile Profile { get; set; }

    //public int? ProfileId { get; set; }
    //public Profile Profile { get; set; }
    //[Required]
    //public int CountryId { get; set; }
    //public Country Country { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }

    public string Password { get; set; }

    //Name = input.Name,
    //Email = input.Email,
    //Password = HashPassword(input.Password)
  

    public int BranchId { get; set; }
    public Branch Branch { get; set; }





    // }

}
