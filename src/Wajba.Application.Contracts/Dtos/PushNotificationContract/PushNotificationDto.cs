namespace Wajba.Dtos.PushNotificationContract;

public class PushNotificationDto:FullAuditedEntityDto<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime Date { get; set; }
    public int? RoleId { get; set; }
    public int? UserId { get; set; }
    public string RoleName { get; set; }
    public string UserName { get; set; }
}
