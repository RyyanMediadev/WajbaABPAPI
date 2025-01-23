global using Wajba.Dtos.UserAddressContract;

namespace Wajba.Controllers
{

    public class UserAddressController : WajbaController
    {
        private readonly IUserAddressAppService _userAddressAppService;

        public UserAddressController(IUserAddressAppService userAddressAppService)
        {
            _userAddressAppService = userAddressAppService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserAddressDto input)
        {
            try
            {
                var result = await _userAddressAppService.CreateAsync(input);
                return Ok(new ApiResponse<UserAddressDto>
                {
                    Success = true,
                    Message = "User address created successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<UserAddressDto>
                {
                    Success = false,
                    Message = $"Error creating user address: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateUserAddressDto input)
        {
            try
            {
                var result = await _userAddressAppService.UpdateAsync(input);
                return Ok(new ApiResponse<UserAddressDto>
                {
                    Success = true,
                    Message = "User address updated successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<UserAddressDto>
                {
                    Success = false,
                    Message = $"Error updating user address: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _userAddressAppService.DeleteAsync(id);
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "User address deleted successfully.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Error deleting user address: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetAllByCustomerAsync(string customerId)
        {
            try
            {
                var result = await _userAddressAppService.GetAllByCustomerAsync(customerId);
                return Ok(new ApiResponse<List<UserAddressDto>>
                {
                    Success = true,
                    Message = "User addresses retrieved successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<List<UserAddressDto>>
                {
                    Success = false,
                    Message = $"Error retrieving user addresses: {ex.Message}",
                    Data = null
                });
            }
        }

        //public async Task<IActionResult>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            {
                try
                {
                    var result = await _userAddressAppService.GetByIdAsync(id);
                    return Ok(new ApiResponse<UserAddressDto>
                    {
                        Success = true,
                        Message = "User address retrieved successfully.",
                        Data = result
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new ApiResponse<UserAddressDto>
                    {
                        Success = false,
                        Message = $"Error retrieving user address: {ex.Message}",
                        Data = null
                    });
                }
            }
        }
    }
}
