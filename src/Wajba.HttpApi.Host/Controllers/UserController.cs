using Wajba.Dtos.UserDTO;
using Wajba.UserAppService;

namespace Wajba.Controllers;


public class UserController : WajbaController
{
    private readonly UserService _UserService;

    public UserController(UserService userAppService)
    {
        _UserService = userAppService;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] UserInfoDTO UserInfoDTO)
    {
        try
        {
            var NewUser = await _UserService.Register(UserInfoDTO);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "User created successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error creating branch: {ex.Message}",
                Data = ex.Message
            });
        }


    }
}




