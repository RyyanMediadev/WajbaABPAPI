global using Wajba.Dtos.TimeSlotsContract;
global using Wajba.TimeSlotsServices;

namespace Wajba.Controllers;

public class TimeSlotController : WajbaController
{

    private readonly ITimeSlotAppService _timeSlotAppService;

    public TimeSlotController(ITimeSlotAppService timeSlotAppService)
    {
        _timeSlotAppService = timeSlotAppService;
    }
     

    private readonly TimeSlotsAppservice _timeSlotsAppservice;

    public TimeSlotController(TimeSlotsAppservice timeSlotsAppservice)
    {
        _timeSlotsAppservice = timeSlotsAppservice;
    }
    [HttpPost("seed")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> SeedData()
    {
        try
        {
            await _timeSlotAppService.SeedTimeSlotsAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "TimeSlots seeded successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error seeding timeslots: {ex.Message}",
                Data = null
            });
        }

    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var timeSlots = await _timeSlotsAppservice.GetAllTimeSlotsAsync();
            return Ok(new ApiResponse<List<TimeSlotDto>>
            {
                Success = true,
                Message = "TimeSlots retrieved successfully.",
                Data = timeSlots
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error retrieving timeslots: {ex.Message}",
                Data = null
            });
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] List<UpdateTimeSlotDto> updateTimeSlotDtos)
    {
        try
        {
            await _timeSlotsAppservice.UpdateTimeSlotsAsync(updateTimeSlotDtos);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "TimeSlots updated successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error updating timeslots: {ex.Message}",
                Data = null
            });
        }
    }
}


