global using Volo.Abp.Identity;
using Microsoft.AspNetCore.Identity;
using Wajba.Models.AddressDomain;
using Wajba.Models.WajbaUserRoleDomain;

namespace Wajba.Models.WajbaUserDomain;

public class WajbaUser : FullAuditedEntity<int>
{

    public string FullName { get; set; }
    public string Email { get; set; }

    public string Phone { get; set; }

    public Status status { get; set; }
    public UserTypes Type { get; set; }
    public GenderType GenderType { get; set; }

    public string? ProfilePhoto { get; set; }
    public string Password { get; set; }
    public int? CustomerRole { get; set; }
    public ICollection<WajbaUserRoles> WajbaUserRoles { get; set; } = new List<WajbaUserRoles>();
    public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
    public WajbaUser()
    {

    }

}
