using AutoMapper;
using Volo.Abp.Uow;
using Wajba.CustomerAppService;
using Wajba.Dtos.CustomerContract;
using Wajba.Dtos.UserDTO;
using static Wajba.CustomerAppService.WajbaUserAppService;
using Wajba.Services.ImageService;
using Wajba.UserAppService;

namespace Wajba.Controllers
{

    public class WajbaUserController : WajbaController
    {
        private readonly WajbaUserAppService _WajbaUserAppService;
        private readonly IUnitOfWork _uow;
        private readonly ISmsSenderService _SMS;
        private readonly IAuthenticateService _authService;
        private readonly ICheckUniqes _checkUniq;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        private readonly UserAppService.TokenAuthenticationService.IUserManagementService _UserManagementService;
        private readonly IMailService _mailService;

        public WajbaUserController(WajbaUserAppService wajbaAppService)
        {
            _WajbaUserAppService = wajbaAppService;
        }
        //[IgnoreAntiforgeryToken]
        [HttpPost("Register")]
        public async Task<ActionResult> Register(CreateUserDto input)
        {
            await _WajbaUserAppService.Register(input);
            return Ok();
        }

        //[HttpPost("LogIn")]
        //public async Task<ActionResult> LogIn(LogInDto input)
        //{
        //    await _WajbaUserAppService.Login(input);
        //    return Ok();
        //}

        [HttpPost, Route("LogIn")]
        public IActionResult LogIn(LogInDto LogInDto)
        {
            try
            {
                if (!ModelState.IsValid)
                { return BadRequest(ModelState); }


                var user = _authService.AuthenticateUser(LogInDto, out string token);

                if (user == null)
                {
                    return BadRequest(new { MessageAr = "! خطأ في كلمة المرور او رقم الجوال", MessageEng = "Incorrect Email or Password  !" });
                }
                if (user.status== Enums.Status.Active)
                {
                    return BadRequest(new { MessageAr = " !الحساب غير مفعل توجه لبريدك الالكتروني للتفعيل", MessageEng = "Account is not Active ;Check Your E-mail to Activate !" });
                }
                if (user != null)
                {
                    //Implement User Profiles



                    user.Password = null;
                    return Ok(new
                    {
                        user,

                        token
                    });
                }
                return BadRequest(new { MessageAr = "خطأ في كلمة المرور او رقم الجوال", MessageEng = "Account is not Active ; Check Your E-mail to Activate" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { MessageAr = ex.Message, MessageEng = ex.Message });

            }
        }



        [HttpPut("update-WajbaUser")]
        public async Task<ActionResult> UpdateWajbaUser(UpdateWajbaUserDto input)
        {
            await _WajbaUserAppService.UpdateUserAsync(input);
            return Ok();
        }

        // 3. Get User
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetWajbaUser(int id)
        {
            var result = await _WajbaUserAppService.GetUserAsync(id);
            return Ok(result);
        }

        // 4. Get User List
        [HttpGet("listWajbaUser")]
        public async Task<ActionResult<PagedResultDto<WajbaUserDto>>> GetWajbaUser([FromQuery] GetUserListDto input)
        {
            var result = await _WajbaUserAppService.GetUserListAsync(input);
            return Ok(result);
        }

        // 5. Delete User
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWajbaUser(int id)
        {
            await _WajbaUserAppService.DeleteUserAsync(id);
            return Ok();
        }
    }
}
