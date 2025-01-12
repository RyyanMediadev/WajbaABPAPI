using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.NotificationContract
{
    public interface INotificationService:IApplicationService
    {
        Task<NotificationDto> CreateAsync(CreateNotificationDto input);
        Task<NotificationDto> UpdateAsync(UpdateNotificationDto input);
        Task<NotificationDto> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<PagedResultDto<NotificationDto>> GetAllAsync(GetNotificationInput input);
    }
}
