namespace Wajba.Models.WajbaUserRoleDomain;

public class UserRole:FullAuditedEntity<int>
{
    public string RoleName { get; set; }
    public ICollection<WajbaUser> UserRoles { get; set; } = new List<WajbaUser>();
}
