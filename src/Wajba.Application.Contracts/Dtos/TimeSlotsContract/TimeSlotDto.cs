namespace Wajba.Dtos.TimeSlotsContract;

public class TimeSlotDto 
{
    public int WeekDay { get; set; }
    public List<TimeSlotDetailDto> TimeSlots { get; set; }
}
