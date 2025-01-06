using Wajba.Dtos.NotificationContract;
using Wajba.NotificationService;
using Wajba.Dtos.NotificationContract;

namespace Wajba.Controllers
{
    public class NotificationController : WajbaController
    {
        private readonly NotificationAppService _notificationAppService;

        public NotificationController(NotificationAppService notificationAppService)
        {
            _notificationAppService = notificationAppService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateNotificationDto input)
        {
            try
            {
                await _notificationAppService.CreateAsync(input);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Notification created successfully.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error creating notification: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateNotificationDto input)
        {
            try
            {
                var updatedNotification = await _notificationAppService.UpdateAsync(input);
                return Ok(new ApiResponse<NotificationDto>
                {
                    Success = true,
                    Message = "Notification updated successfully.",
                    Data = updatedNotification
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Notification not found.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error updating notification: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var notification = await _notificationAppService.GetAsync(id);
                return Ok(new ApiResponse<NotificationDto>
                {
                    Success = true,
                    Message = "Notification retrieved successfully.",
                    Data = notification
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Notification not found.",
                    Data = null
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetNotificationInput input)
        {
            try
            {
                var notifications = await _notificationAppService.GetAllAsync(input);
                return Ok(new ApiResponse<PagedResultDto<NotificationDto>>
                {
                    Success = true,
                    Message = "Notifications retrieved successfully.",
                    Data = notifications
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error retrieving notifications: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _notificationAppService.DeleteAsync(id);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Notification deleted successfully.",
                    Data = null
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Notification not found.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error deleting notification: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}