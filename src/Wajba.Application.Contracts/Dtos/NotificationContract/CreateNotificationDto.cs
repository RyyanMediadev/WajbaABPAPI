namespace Wajba.Dtos.NotificationContract
{
    public class CreateNotificationDto
    {
        public string FireBasePublicVapidKey { get; set; }
        public string FireBaseAPIKey { get; set; }
        public string FireBaseProjectId { get; set; }
        public string FireBaseAuthDomain { get; set; }
        public string FireBaseStorageBucket { get; set; }
        public string FireBaseMessageSenderId { get; set; }
        public string FireBaseAppId { get; set; }
        public string FireBaseMeasurementId { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
