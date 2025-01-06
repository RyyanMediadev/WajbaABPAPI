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

        public async Task<NotificationDto> CreateAsync(CreateNotificationDto input)
        {
            var imageUrl = await _imageService.UploadAsync(input.ImageUrl);
            var notification = ObjectMapper.Map<CreateNotificationDto, Notification>(input);
            notification.ImageUrl = imageUrl;

            await _notificationRepository.InsertAsync(notification);
            return ObjectMapper.Map<Notification, NotificationDto>(notification);
        }

        public async Task<NotificationDto> UpdateAsync(UpdateNotificationDto input)
        {
            var notification = await _notificationRepository.GetAsync(input.id);
            if (notification == null)
            {
                throw new EntityNotFoundException(typeof(Notification), input.id);
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
        public async Task<PagedResultDto<NotificationDto>> GetAllAsync(GetNotificationInput input)
        {
            var queryable = await _notificationRepository.GetQueryableAsync();

            queryable = queryable.WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                n => n.FireBasePublicVapidKey.Contains(input.Filter) ||
                     n.FireBaseAPIKey.Contains(input.Filter) ||
                     n.FireBaseProjectId.Contains(input.Filter) ||
                     n.ImageUrl.Contains(input.Filter)
            );

            var totalCount = await AsyncExecuter.CountAsync(queryable);
            var items = await AsyncExecuter.ToListAsync(queryable
                .OrderBy(input.Sorting ?? nameof(Notification.FireBasePublicVapidKey))
                .PageBy(input.SkipCount, input.MaxResultCount));

            return new PagedResultDto<NotificationDto>(
                totalCount,
                ObjectMapper.Map<List<Notification>, List<NotificationDto>>(items)
            );
        }
    }
}