global using Wajba.Models.WajbaUserDomain;

namespace Wajba.Models.PushNotificationDomains;

public class PushNotification : FullAuditedEntity<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    //public int? RoleId { get; set; }
    public DateTime? Date { get; set; }
    [ForeignKey("UserId")]
    public int WajbaUserId { get; set; }
    public WajbaUser WajbaUser { get; set; }
}