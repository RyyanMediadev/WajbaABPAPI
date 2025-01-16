global using Volo.Abp.Identity;
using Microsoft.AspNetCore.Identity;
using Wajba.Enums;
using Wajba.Models.AddressDomain;

namespace Wajba.Models.UsersDomain;

public class APPUser : IdentityUser<Guid>, IEntity<Guid>
{


   
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }

    public string? FullName { get; set; }
    public Status status { get; set; }
    public UserTypes Type { get; set; }
    public string? ProfilePhoto { get; set; }
    public int Points { get; set; } = 0;
    public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
    public ICollection<UserAddress>? Addresses { get; set; } = new List<UserAddress>();

    public ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public Guid Id { get; set; }

    public object[] GetKeys()
    {
        throw new NotImplementedException();
    }
}
