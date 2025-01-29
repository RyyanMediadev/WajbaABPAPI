using Wajba.Dtos.PushNotificationContract;
using Wajba.PushNotificationServices;
using Wajba.SiteService;

namespace Wajba.Controllers;

public class PushNotificationsController : WajbaController
{
    private readonly PushNotificationAppservices _pushNotification;

    public PushNotificationsController(PushNotificationAppservices pushNotification)
    {
        _pushNotification = pushNotification;
    }
    [HttpPost]
    public async Task<ActionResult<PushNotificationDto>> Create(CreatePushNotificationDto pushNotificationDto)
    {
        try
        {
            PushNotificationDto dto = await _pushNotification.CreateAsync(pushNotificationDto);
            return Ok(new ApiResponse<PushNotificationDto>
            {
                Success = true,
                Message = "Site created successfully.",
                Data = dto
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating site: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpPut]
    public async Task<ActionResult<PushNotificationDto>> Update(UpdatePushNotificationDto dto)
    {
        try
        {
            PushNotificationDto dto = await _pushNotification.(pushNotificationDto);
            return Ok(new ApiResponse<PushNotificationDto>
            {
                Success = true,
                Message = "Site created successfully.",
                Data = dto
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating site: {ex.Message}",
                Data = null
            });
        }

    }
}