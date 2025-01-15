using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wajba.CustomerAppService;
using Wajba.Dtos.CustomerContract;

namespace Wajba.Controllers
{
   
    public class CustomerController : WajbaController
    {
        private readonly CustomUserAppService _userAppService;

        public CustomerController(CustomUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        [IgnoreAntiforgeryToken]
        [HttpPost("Create-user")]
        public async Task<ActionResult> CreateUser(CreateUserDto input)
        {
            await _userAppService.CreateuserAsync(input);
            return Ok();
        }

        [HttpPut("update-user")]
        public async Task<ActionResult> UpdateUser(UpdateUserDto input)
        {
            await _userAppService.UpdateUserAsync(input);
            return Ok();
        }

        // 3. Get User
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUser(Guid id)
        {
            var result = await _userAppService.GetUserAsync(id);
            return Ok(result);
        }

        // 4. Get User List
        [HttpGet("list")]
        public async Task<ActionResult<PagedResultDto<GetUserDto>>> GetUsers([FromQuery] GetUserListDto input)
        {
            var result = await _userAppService.GetUserListAsync(input);
            return Ok(result);
        }

        // 5. Delete User
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userAppService.DeleteUserAsync(id);
            return Ok();
        }
    }
}
