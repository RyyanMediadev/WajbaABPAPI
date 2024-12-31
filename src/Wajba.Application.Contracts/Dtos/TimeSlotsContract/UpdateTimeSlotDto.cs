namespace Wajba.Dtos.TimeSlotsContract;

public class UpdateTimeSlotDto
{
    public DayOfWeek WeekDay { get; set; }
    public List<TimeSlotUpdateDetailDto> TimeSlots { get; set; }
}
