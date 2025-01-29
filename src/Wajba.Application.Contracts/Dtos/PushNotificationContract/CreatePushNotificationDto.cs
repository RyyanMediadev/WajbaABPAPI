namespace Wajba.Dtos.PushNotificationContract;

public class CreatePushNotificationDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Base64ImageModel? ImageUrl { get; set; }
    public DateTime Date { get; set; }
    //public int RoleId { get; set; }
    public int UserId { get; set; }

}
