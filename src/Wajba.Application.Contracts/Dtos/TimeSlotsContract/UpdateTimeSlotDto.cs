namespace Wajba.Dtos.TimeSlotsContract;

public class UpdateTimeSlotDto
{
    public int WeekDay { get; set; }
    public List<TimeSlotUpdateDetailDto> TimeSlots { get; set; }
}
