namespace Wajba.Dtos.TimeSlotsContract;

public class TimeSlotDto 
{
    public DayOfWeek WeekDay { get; set; }
    public List<TimeSlotDetailDto> TimeSlots { get; set; }
}
