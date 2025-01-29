namespace Wajba.Models.WajbaUserRoleDomain;

public class WajbaUserRoles : FullAuditedEntity<int>
{
    public int WajbaUserId { get; set; }
    public WajbaUser WajbaUser { get; set; }
    public int? RoleId { get; set; }
    public UserRole UserRole { get; set; }
}
