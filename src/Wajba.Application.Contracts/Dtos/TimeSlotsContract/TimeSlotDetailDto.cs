namespace Wajba.Dtos.TimeSlotsContract
{
    public class TimeSlotDetailDto
    {
        public int Id { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
    }
}
