global using Wajba.Dtos.PushNotificationContract;
global using Wajba.PushNotificationServices;

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
                Message = "PushNotification created successfully.",
                Data = dto
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating PushNotification: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpPut]
    public async Task<ActionResult<PushNotificationDto>> Update(UpdatePushNotificationDto dto)
    {
        try
        {
            PushNotificationDto notificationDto = await _pushNotification.Updtate(dto);
            return Ok(new ApiResponse<PushNotificationDto>
            {
                Success = true,
                Message = "PushNotification Updated successfully.",
                Data = notificationDto
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error Update PushNotification: {ex.Message}",
                Data = null
            });
        }

    }
    [HttpGet]
    public async Task<ActionResult<PagedResultDto<PushNotificationDto>>> GetAll(GetPushnotificationinput get)
    {
        try
        {
            var notificationDto = await _pushNotification.GetAll(get);
            return Ok(new ApiResponse<PagedResultDto<PushNotificationDto>>
            {
                Success = true,
                Message = "PushNotifications retrived successfully.",
                Data = notificationDto
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrived PushNotifications: {ex.Message}",
                Data = null
            });
        }

    }
    [HttpGet("GetbyId{id}")]
    public async Task<ActionResult<PushNotificationDto>> GetById(int id)
    {
        try
        {
            PushNotificationDto notificationDto = await _pushNotification.Getbyid(id);
            return Ok(new ApiResponse<PushNotificationDto>
            {
                Success = true,
                Message = "PushNotification retrived successfully.",
                Data = notificationDto
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrived PushNotification: {ex.Message}",
                Data = null
            });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _pushNotification.Delete(id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Pushnotification deleted successfully.",
                Data = null
            });
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Pushnotification not found.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error deleting Pushnotification: {ex.Message}",
                Data = null
            });
        }
    }
}