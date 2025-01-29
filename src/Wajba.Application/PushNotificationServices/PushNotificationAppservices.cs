global using Wajba.Models.PushNotificationDomains;
global using Wajba.Dtos.PushNotificationContract;
global using Wajba.Models.WajbaUserDomain;

namespace Wajba.PushNotificationServices;

[RemoteService(false)]
public class PushNotificationAppservices : ApplicationService
{
    private readonly IRepository<PushNotification, int> _pushnotificationrepo;
    private readonly IRepository<WajbaUser, int> _userrepo;
    private readonly IImageService _imageService;

    public PushNotificationAppservices(IRepository<PushNotification, int> pushnotificationrepo,
        IRepository<WajbaUser, int> userrepo,
         IImageService imageService)
    {
        _pushnotificationrepo = pushnotificationrepo;
        _userrepo = userrepo;
        _imageService = imageService;
    }
    public async Task<PushNotificationDto> CreateAsync(CreatePushNotificationDto dto)
    {
        var user = await _userrepo.FirstOrDefaultAsync(p => p.Id == dto.UserId);
        if (user == null)
            throw new Exception("Invalid data");
        PushNotification pushNotification = new PushNotification()
        {
            Title = dto.Title,
            Description = dto.Description,
            Date = dto.Date,
            WajbaUser = user,
            WajbaUserId = dto.UserId,
        };
        var imagebytes = Convert.FromBase64String(dto.ImageUrl.Base64Content);
        using var ms = new MemoryStream(imagebytes);
        pushNotification.ImageUrl = await _imageService.UploadAsync(ms, dto.ImageUrl.FileName);
        await _pushnotificationrepo.InsertAsync(pushNotification, true);
        return topushNotificationDto(pushNotification);
    }
    public async Task<PushNotificationDto> Updtate(UpdatePushNotificationDto dto)
    {
        PushNotification pushNotification = await _pushnotificationrepo.FirstOrDefaultAsync(p => p.Id == dto.Id);
        if (pushNotification == null)
            throw new Exception("Not Found");
        WajbaUser wajbaUser = await _userrepo.FirstOrDefaultAsync(p => p.Id == dto.Id);
        if (wajbaUser == null)
            throw new Exception("User not found");
        pushNotification.WajbaUser = wajbaUser;
        pushNotification.WajbaUserId = dto.UserId;
        pushNotification.Title = dto.Title;
        pushNotification.Date = dto.Date;
        pushNotification.Description = dto.Description;
        var imagebytes = Convert.FromBase64String(dto.ImageUrl.Base64Content);
        using var ms = new MemoryStream(imagebytes);
        pushNotification.ImageUrl = await _imageService.UploadAsync(ms, dto.ImageUrl.FileName);
        pushNotification.LastModificationTime = DateTime.UtcNow;
        await _pushnotificationrepo.UpdateAsync(pushNotification,true);
        return topushNotificationDto(pushNotification);
    }

    public async Task<PagedResultDto<PushNotificationDto>> GetAll(GetPushnotificationinput pushnotificationinput)
    {
        var notifications = await _pushnotificationrepo.WithDetailsAsync(p => p.WajbaUser);
        List<PushNotificationDto> pushNotificationDtos = new List<PushNotificationDto>();
        foreach (var i in notifications)
            pushNotificationDtos.Add(topushNotificationDto(i));
        return new PagedResultDto<PushNotificationDto>(pushNotificationDtos.Count(), pushNotificationDtos);
    }
    public async Task Delete(int id)
    {
        PushNotification push = await _pushnotificationrepo.FindAsync(id);
        if (push == null)
            throw new Exception("Not Found");
        await _pushnotificationrepo.HardDeleteAsync(push, true);
    }
    private static PushNotificationDto topushNotificationDto(PushNotification pushNotification)
    {
        return new PushNotificationDto()
        {
            Description = pushNotification.Description,
            ImageUrl = pushNotification.ImageUrl,
            Title = pushNotification.Title,
            UserName = pushNotification.WajbaUser.FullName,
            Date = (DateTime)pushNotification.Date,
        };
    }
}