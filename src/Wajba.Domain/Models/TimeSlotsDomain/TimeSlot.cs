namespace Wajba.Models.TimeSlotsDomain;
public class TimeSlot:FullAuditedEntity<int>
{
    public DayOfWeek WeekDay { get; set; }
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }

    public TimeSlot()
    {

    }
}