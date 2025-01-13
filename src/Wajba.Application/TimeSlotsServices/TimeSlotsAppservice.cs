global using System;
global using Wajba.Dtos.TimeSlotsContract;
global using Wajba.Models.TimeSlotsDomain;

namespace Wajba.TimeSlotsServices;

[RemoteService(false)]

public class TimeSlotsAppservice : ApplicationService, ITimeSlotAppService
{
    private readonly IRepository<TimeSlot, int> _timeSlotRepository;

    public TimeSlotsAppservice(IRepository<TimeSlot, int> timeSlotRepository)
    {
        _timeSlotRepository = timeSlotRepository;
    }
    public async Task SeedTimeSlotsAsync()
    {
        var timeSlots = new List<TimeSlot>
    {
        new TimeSlot { WeekDay = DayOfWeek.Sunday, OpeningTime = TimeSpan.Parse("08:00"), ClosingTime = TimeSpan.Parse("10:00") },
        new TimeSlot { WeekDay = DayOfWeek.Monday, OpeningTime = TimeSpan.Parse("11:00"), ClosingTime = TimeSpan.Parse("13:00") },
        new TimeSlot { WeekDay = DayOfWeek.Tuesday, OpeningTime = TimeSpan.Parse("09:00"), ClosingTime = TimeSpan.Parse("11:00") },
        new TimeSlot { WeekDay = DayOfWeek.Tuesday, OpeningTime = TimeSpan.Parse("13:00"), ClosingTime = TimeSpan.Parse("15:00") },
        new TimeSlot { WeekDay = DayOfWeek.Wednesday, OpeningTime = TimeSpan.Parse("10:00"), ClosingTime = TimeSpan.Parse("12:00") },
        new TimeSlot { WeekDay = DayOfWeek.Wednesday, OpeningTime = TimeSpan.Parse("14:00"), ClosingTime = TimeSpan.Parse("16:00") },
        new TimeSlot { WeekDay = DayOfWeek.Thursday, OpeningTime = TimeSpan.Parse("08:00"), ClosingTime = TimeSpan.Parse("10:00") },
        new TimeSlot { WeekDay = DayOfWeek.Friday, OpeningTime = TimeSpan.Parse("09:00"), ClosingTime = TimeSpan.Parse("11:00") }
    };

        foreach (var slot in timeSlots)
        {
            
                await _timeSlotRepository.InsertAsync(slot);
            
        }
    }
    public async Task<List<TimeSlotDto>> GetAllTimeSlotsAsync()
    {
        var timeSlots = await _timeSlotRepository.GetListAsync();
        if (timeSlots == null || !timeSlots.Any())
        {
            // Handle the case where no timeslots are available
            return new List<TimeSlotDto>();
        }
        var groupedSlots = timeSlots
            .GroupBy(s => s.WeekDay)
            .Select(g => new TimeSlotDto
            {
                WeekDay = g.Key,
                TimeSlots = g.Select(s => new TimeSlotDetailDto
                {
                    Id = s.Id,
                    OpeningTime = s.OpeningTime,
                    ClosingTime = s.ClosingTime
                }).ToList()
            }).ToList();

        return groupedSlots;
    }

    public async Task UpdateTimeSlotsAsync(List<UpdateTimeSlotDto> updateTimeSlotDtos)
    {
        foreach (var updateTimeSlotDto in updateTimeSlotDtos)
        {
            var existingSlots = await _timeSlotRepository.GetListAsync(x => x.WeekDay == updateTimeSlotDto.WeekDay);

            foreach (var slotDto in updateTimeSlotDto.TimeSlots)
            {
                var existingSlot = existingSlots.FirstOrDefault(s => s.Id == slotDto.Id);

                if (existingSlot != null)
                {
                    existingSlot.OpeningTime = TimeSpan.Parse(slotDto.OpeningTime);
                    existingSlot.ClosingTime = TimeSpan.Parse(slotDto.ClosingTime);
                }
                else
                {
                    await _timeSlotRepository.InsertAsync(new TimeSlot
                    {
                        WeekDay = updateTimeSlotDto.WeekDay,
                        OpeningTime = TimeSpan.Parse(slotDto.OpeningTime),
                        ClosingTime = TimeSpan.Parse(slotDto.ClosingTime)
                    });
                }
            }

            var updatedIds = updateTimeSlotDto.TimeSlots.Select(s => s.Id).ToList();
            var slotsToRemove = existingSlots.Where(s => !updatedIds.Contains(s.Id)).ToList();

            foreach (var slotToRemove in slotsToRemove)
            {
                await _timeSlotRepository.DeleteAsync(slotToRemove);
            }
        }
    }
}
