using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.TimeSlotsContract
{
    public class TimeSlotDetailDto
    {
        public int Id { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
    }
}
