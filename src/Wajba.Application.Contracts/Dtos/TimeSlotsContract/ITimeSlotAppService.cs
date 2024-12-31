using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.TimeSlotsContract
{
    public interface ITimeSlotAppService : IApplicationService
    {
        Task<List<TimeSlotDto>> GetAllTimeSlotsAsync();
        Task UpdateTimeSlotsAsync(List<UpdateTimeSlotDto> updateTimeSlotDtos);
    }

}
