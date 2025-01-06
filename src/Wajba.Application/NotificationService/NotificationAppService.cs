using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.Dtos.NotificationContract;
using Wajba.Models.NotificationDomain;

namespace Wajba.NotificationService
{
    public class NotificationAppService : ApplicationService
    {
        private readonly IRepository<Notification, int> _notificationRepository;
        private readonly IImageService _imageService;

        public NotificationAppService(
            IRepository<Notification, int> notificationRepository,
            IImageService imageService)
        {
            _notificationRepository = notificationRepository;
            _imageService = imageService;
        }

        public async Task<NotificationDto> CreateAsync(CreateUpdateNotificationDto input)
        {
            var imageUrl = await _imageService.UploadAsync(input.ImageUrl);
            var notification = ObjectMapper.Map<CreateUpdateNotificationDto, Notification>(input);
            notification.ImageUrl = imageUrl;

            await _notificationRepository.InsertAsync(notification);
            return ObjectMapper.Map<Notification, NotificationDto>(notification);
        }

        public async Task<NotificationDto> UpdateAsync(int id, CreateUpdateNotificationDto input)
        {
            var notification = await _notificationRepository.GetAsync(id);
            if (notification == null)
            {
                throw new EntityNotFoundException(typeof(Notification), id);
            }

            var imageUrl = await _imageService.UploadAsync(input.ImageUrl);
            ObjectMapper.Map(input, notification);
            notification.ImageUrl = imageUrl;

            await _notificationRepository.UpdateAsync(notification);
            return ObjectMapper.Map<Notification, NotificationDto>(notification);
        }

        public async Task<NotificationDto> GetAsync(int id)
        {
            var notification = await _notificationRepository.GetAsync(id);
            return ObjectMapper.Map<Notification, NotificationDto>(notification);
        }

        public async Task DeleteAsync(int id)
        {
            await _notificationRepository.DeleteAsync(id);
        }

        public async Task<List<NotificationDto>> GetAllAsync()
        {
            var notifications = await _notificationRepository.GetListAsync();
            return ObjectMapper.Map<List<Notification>, List<NotificationDto>>(notifications);
        }
    }
}
