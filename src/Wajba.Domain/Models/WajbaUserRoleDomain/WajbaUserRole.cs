using Wajba.Models.WajbaUserDomain;

namespace Wajba.Models.WajbaUserRoleDomain;

public class WajbaUserRole : FullAuditedEntity<int>
{
	public int WajbaUserId { get; set; }
	public WajbaUser WajbaUser { get; set; }

	public int? RoleId { get; set; }
}
