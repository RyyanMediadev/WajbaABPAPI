using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wajba.Dtos.NotificationContract;
using Wajba.NotificationService;

namespace Wajba.Controllers
{
    public class NotificationController :WajbaController
    {
        private readonly NotificationAppService _notificationAppService;

        public NotificationController(NotificationAppService notificationAppService)
        {
            _notificationAppService = notificationAppService;
        }

        [HttpPost]
        public async Task<NotificationDto> CreateAsync([FromBody] CreateUpdateNotificationDto input)
        {
            return await _notificationAppService.CreateAsync(input);
        }

        [HttpPut("{id}")]
        public async Task<NotificationDto> UpdateAsync(int id, [FromBody] CreateUpdateNotificationDto input)
        {
            return await _notificationAppService.UpdateAsync(id, input);
        }

        [HttpGet("{id}")]
        public async Task<NotificationDto> GetAsync(int id)
        {
            return await _notificationAppService.GetAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _notificationAppService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<ListResultDto<NotificationDto>> GetAllAsync()
        {
            var result = await _notificationAppService.GetAllAsync();
            return new ListResultDto<NotificationDto>(result);
        }
    }
}
