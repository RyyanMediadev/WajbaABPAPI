

using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Volo.Abp.Uow;
using Wajba.Dtos.CustomerContract;
using Wajba.Dtos.WajbaUsersContract;
using Wajba.Models.WajbaUserDomain;
using Wajba.UserManagment;
using Wajba.WajbaUsersService;

namespace Wajba.Controllers
{

    public class WajbaUserController : WajbaController
    {
        private readonly WajbaUsersAppservice _WajbaUsersAppService;

        //private readonly IUnitOfWork _uow;
        //private readonly ISmsSenderService _SMS;
        //private readonly IAuthenticateService _authService;
        //private readonly ICheckUniqes _checkUniq;
        //private readonly IHostingEnvironment _hostingEnvironment;
        //private readonly IMapper _mapper;
        //private readonly UserManagment.TokenAuthenticationService.IUserManagementService _UserManagementService;
        //private readonly IMailService _mailService;

        public WajbaUserController(WajbaUsersAppservice wajbaAppService)
        {
            _WajbaUsersAppService = wajbaAppService;
        }
        //[IgnoreAntiforgeryToken]
        [HttpPost("Register")]
        public async Task<ActionResult> Register(CreateUserDto input)
        {
            await _WajbaUsersAppService.Register(input);
            return Ok();
        }





        [HttpPost, Route("LogIn")]
        public IActionResult LogIn(LogInDto LogInDto)
        {
            try
            {
                if (!ModelState.IsValid)
                { return BadRequest(ModelState); }


                //    var user = _authService.AuthenticateUser(LogInDto, out string token);
                var user = _WajbaUsersAppService.AuthenticateUser(LogInDto, out string token);


                if (user == null)
                {
                    return BadRequest(new { MessageAr = "! خطأ في كلمة المرور او رقم الجوال", MessageEng = "Incorrect Email or Password  !" });
                }
                if (user.status == Enums.Status.Active)
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
            await _WajbaUsersAppService.UpdateUserAsync(input);
            return Ok();
        }

        //// 3. Get User
        //[HttpGet("{id}")]
        //public async Task<ActionResult<GetUserDto>> GetWajbaUser(int id)
        //{
        //    var result = await _WajbaUsersAppService.GetUserAsync(id);
        //    return Ok(result);
        //}

        //// 4. Get User List
        //[HttpGet("listWajbaUser")]
        //public async Task<ActionResult<PagedResultDto<WajbaUserDto>>> GetWajbaUser([FromQuery] GetUserListDto input)
        //{
        //    var result = await _WajbaUsersAppService.GetUserListAsync(input);
        //    return Ok(result);
        //}

        //// 5. Delete User
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteWajbaUser(int id)
        //{
        //    await _WajbaUsersAppService.DeleteUserAsync(id);
        //    return Ok();
        //}
    }
}
