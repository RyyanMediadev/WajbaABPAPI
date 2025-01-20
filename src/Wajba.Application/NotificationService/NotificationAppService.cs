global using Wajba.Dtos.NotificationContract;
global using Wajba.Models.NotificationDomain;
using System.IO;

namespace Wajba.NotificationService;

[RemoteService(false)]
public class NotificationAppService : ApplicationService, INotificationService
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
        var notification = ObjectMapper.Map<CreateNotificationDto, Notification>(input);
        if (input.Model != null)
        {
            var imagebytes = Convert.FromBase64String(input.Model.Base64Content);
            using var ms = new MemoryStream(imagebytes);
            notification.ImageUrl = await _imageService.UploadAsync(ms, input.Model.FileName);
        }
        await _notificationRepository.InsertAsync(notification, true);
        return ObjectMapper.Map<Notification, NotificationDto>(notification);
    }

    public async Task<NotificationDto> UpdateAsync(UpdateNotificationDto input)
    {
        var notification = await _notificationRepository.GetAsync(input.id);
        if (notification == null)
            throw new EntityNotFoundException(typeof(Notification), input.id);
        ObjectMapper.Map(input, notification);
        if (input.Model != null)
        {
            var imagebytes = Convert.FromBase64String(input.Model.Base64Content);
            using var ms = new MemoryStream(imagebytes);
            notification.ImageUrl = await _imageService.UploadAsync(ms, input.Model.FileName);
        }
        notification.LastModificationTime = DateTime.UtcNow;
        await _notificationRepository.UpdateAsync(notification, true);
        return ObjectMapper.Map<Notification, NotificationDto>(notification);
    }

    public async Task<NotificationDto> GetAsync(int id)
    {
        var notification = await _notificationRepository.GetAsync(id);
        if (notification == null)
            throw new EntityNotFoundException(typeof(Notification), id);
        return ObjectMapper.Map<Notification, NotificationDto>(notification);
    }

    public async Task DeleteAsync(int id)
    {
        Notification notification = await _notificationRepository.GetAsync(id);
        if (notification == null)
            throw new EntityNotFoundException(typeof(Notification), id);
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