

using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Volo.Abp.Uow;
using Wajba.Categories;
using Wajba.Dtos.CustomerContract;
using Wajba.Dtos.UserDTO;
using Wajba.Dtos.WajbaUsersContract;
using Wajba.Models.CouponsDomain;
using Wajba.Models.WajbaUserDomain;
using Wajba.UserAppService;
using Wajba.UserManagment;
using Wajba.WajbaUsersService;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using GetUserDto = Wajba.Dtos.WajbaUsersContract.GetUserDto;

namespace Wajba.Controllers
{

    public class WajbaUserController : WajbaController
    {
        private readonly WajbaUsersAppservice _WajbaUsersAppService;

        //private readonly IUnitOfWork _uow;

        // private readonly ISmsSenderService _SMS;
        //private readonly IAuthenticateService _authService;
        //private readonly ICheckUniqes _checkUniq;
        //private readonly IHostingEnvironment _hostingEnvironment;
        //private readonly IMapper _mapper;
        //private readonly UserManagment.TokenAuthenticationService.IUserManagementService _UserManagementService;
        ///private readonly IMailService _mailService;
        //Confirm Uploads

        public WajbaUserController(WajbaUsersAppservice wajbaAppService)
        {
            _WajbaUsersAppService = wajbaAppService;
        }
        //[IgnoreAntiforgeryToken]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateUserDto input)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // var MessageArEng = _checkUniq.CheckUniqeValue(new DLayer.DTOs.SharedDTO.UniqeDTO { Email = userDto.Email, Mobile = userDto.Phone });
                    //if (MessageArEng.Count() > 0)
                    //{
                    //    return BadRequest("Exists Email or Phone");
                    //}

                    await _WajbaUsersAppService.Register(input);

                    return Ok(new ApiResponse<CreateUserDto>
                    {
                        Success = true,
                        Message = "User created successfully.",
                        Data = input
                    });

                }
                catch (Exception ex)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Error creating coupon: {ex.Message}",
                        Data = null
                    });
                    //  return BadRequest(" Internal server error" + " " + ex.Message);

                }

            }
            return BadRequest(new { MessageAr = "إسم المستخدم أو الرقم السري غير صحيح !", MessageEng = "Invalid Username or Password !" });

        }





        [HttpPost, Route("LogIn")]
        public async Task<IActionResult> LogInAsync(LogInWajbaUserDto LogInDto)
        {
            try
            {
                if (!ModelState.IsValid)
                { return BadRequest(ModelState); }

              
                var user = await _WajbaUsersAppService.AuthenticateUser(LogInDto);


                if (user == null)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"User Not Found",
                        Data = null
                    });

                }
                //if (user.status == Enums.Status.InActive)
                //{
                //	return BadRequest(new ApiResponse<object>
                //	{
                //		Success = false,
                //		Message = $"Account is not Active ;Check Your E-mail to Activate",
                //		Data = null
                //	});

                //}
                if (LogInDto.LogInAPPCode  == "EndUSerApp@SpotIdeas" &&LogInDto.Phone !=null)
                {


                    var ExistUSer = await _WajbaUsersAppService.ValidateOtpUser(LogInDto.Phone);
                    if (ExistUSer != null)
                    {
                        try
                        {
                            //VerifyCodeHelper verifyCodeHelper = new VerifyCodeHelper(_uow, _SMS, _mailService);
                            _WajbaUsersAppService.SendOTP(ExistUSer.Phone, ExistUSer.Id, ExistUSer.Email);

                            return Ok(new ApiResponse<LogInWajbaUserDto>
                            {
                                Success = true,
                                Message = "Code Sent to your Phone  successfully.",
                                Data = null
                            });
                        }
                        catch (Exception ex)
                        {


                            return BadRequest(new { erorr = ex });

                        }
                    }

                }

                if (LogInDto.LogInAPPCode == "DashBoardweb@SpotIdeas" && LogInDto.Email != null)
                {


                    var ExistUSer = await _WajbaUsersAppService.IsValidUserAsync(LogInDto);
                    if (ExistUSer != null)
                    {
                        try
                        {
                            ////VerifyCodeHelper verifyCodeHelper = new VerifyCodeHelper(_uow, _SMS, _mailService);
                            //_WajbaUsersAppService.SendOTP(ExistUSer.Phone, ExistUSer.Id, ExistUSer.Email);
                            var GenerateTokenAsync = _WajbaUsersAppService.GenerateTokenAsync(ExistUSer);

                            //return Ok(new ApiResponse<LogInWajbaUserDto>
                            //{
                            //    Success = true,
                            //    Message = "User Logged in succesfully ..",
                            //    Data = ExistUSer
                            //});

                            return Ok(new
                            {
                                WajbaUser = user,

                                GenerateToken = GenerateTokenAsync
                            });
                        }
                        catch (Exception ex)
                        {


                            return BadRequest(new { erorr = ex });

                        }
                    }

                }
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"User Details Not Found ,,Wrong Data !",
                    Data = null
                });



            }
            catch (Exception ex)
            {
                return BadRequest(new { MessageAr = ex.Message, MessageEng = ex.Message });

            }
        }






        [AllowAnonymous]
        [HttpPost, Route("VerifyOTPCode")]
        public async Task<IActionResult> VerifyOTPCode(int VerifyOTPCode, LogInWajbaUserDto loginDto)
        {
            var user = await _WajbaUsersAppService.IsValidUserAsync(loginDto);

            var GtResult = _WajbaUsersAppService.ActivateOTP(VerifyOTPCode);

            if (GtResult)
            {
                //return Ok(GtResult);

                if (user != null)
                {
                    //Implement User Profiles

                    ////user.Password = null;


                    //return Ok(new ApiResponse<UserInfoDTO>
                    //{
                    //    Success = true,
                    //    Message = "User created successfully.",
                    //    Data = user
                    //});
                    var GenerateTokenAsync = _WajbaUsersAppService.GenerateTokenAsync(user);

                    return Ok(new
                    {
                        WajbaUser = user,

                        GenerateToken = GenerateTokenAsync
                    });
                }
                return BadRequest(new { MessageAr = "خطأ في كلمة المرور او رقم الجوال", MessageEng = "Account is not Active ; Check Your E-mail to Activate" });
            }

            return BadRequest("Activation Error ... ");

        }


        //[AllowAnonymous]
        //[HttpPost, Route("SendOTPCode")]
        //public async Task<IActionResult> SendOTPCode(string UserPhone)
        //{


        //	var ExistUSer = await _WajbaUsersAppService.ValidateOtpUser(UserPhone);
        //	if (ExistUSer != null)
        //	{
        //		try
        //		{
        //			//VerifyCodeHelper verifyCodeHelper = new VerifyCodeHelper(_uow, _SMS, _mailService);
        //			_WajbaUsersAppService.SendOTP(ExistUSer.Phone, ExistUSer.Id, ExistUSer.Email);

        //			return Ok(new { Status = "Sent" });
        //		}
        //		catch (Exception ex)
        //		{

        //			return BadRequest(new { erorr = ex });

        //		}
        //	}

        //	return BadRequest("User Details Not Found ,,Wrong Data !");

        //}

        [HttpPut("AccountInfoEdit")]
        public async Task<IActionResult> AccountInfoEdit(AccountInfoEditByWajbaUserId AccountInfoEditByWajbaUserId)
        {


            try
            {
                var accountInfo = await _WajbaUsersAppService.AccountInfoEdit(AccountInfoEditByWajbaUserId);

                return Ok(new ApiResponse<AccountInfoEditByWajbaUserId>
                {
                    Success = true,
                    Message = "Account Info updated successfully.",
                    Data = accountInfo
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "User not found.",
                    Data = null
                });
            }
        }
        //[HttpGet("{id}")]
        [HttpGet, Route("AccountInfoGetByWajbaUserId")]

        public async Task<ActionResult<GetUserDto>> AccountInfoGetByWajbaUserId(int id)
        {
            // return Ok(result);

            if (ModelState.IsValid)
            {
                try
                {


                    var result = await _WajbaUsersAppService.AccountInfoGetByWajbaUserId(id);

                    return Ok(new ApiResponse<GetUserDto>
                    {
                        Success = true,
                        Message = "User Retrived successfully.",
                        Data = result
                    });

                }
                catch (Exception ex)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Error Retrived User: {ex.Message}",
                        Data = null
                    });
                    //  return BadRequest(" Internal server error" + " " + ex.Message);

                }

            }
            return BadRequest(new { MessageAr = "مستخدم غير موجود", MessageEng = "Invalid User !" });

        }


        //[HttpPut("update-WajbaUser")]
        //public async Task<IActionResult> UpdateWajbaUser(UpdateWajbaUserDto input)
        //{
        //    await _WajbaUsersAppService.UpdateUserAsync(input);
        //    return Ok();
        //}

        [AllowAnonymous]
        [HttpPost, Route("ResendActivation")]
        public IActionResult ResendActivation(string Phone)
        {
            if (Phone != null)
            {
                //// var User = _uow.UserRepository.GetMany(a => a.Phone == Phone).FirstOrDefault();
                // if (User != null)
                // {

                //     VerifyCodeHelper VerifyCodeHelper = new VerifyCodeHelper(_uow, (ISMS)_SMS, _mailService);
                //     VerifyCodeHelper.SendOTP(User.Phone, User.Id, User.Email);



                //     return Ok(new { MessageEng = "Activation   is Sent to your Phone " });
                // }

                return BadRequest("Email Is not Found");

            }
            else
            {
                return BadRequest("Invalid Data ,,Enter Your Email");
            }


        }



        [AllowAnonymous]
        [HttpPost, Route("getAllOtpCodes")]

        public async Task<IActionResult> getAllOtpCodes()
        {
            //var AllCodes = _uow.VerifyCodeRepository.GetAll().OrderByDescending(i => i.Id);


            //return Ok(new { AllCodes = AllCodes });

            return Ok();

        }

        [AllowAnonymous]
        [HttpPost, Route("ForgetPasswordPost")]
        public async Task<IActionResult> ForgetPasswordPost([FromBody] ForgetPasswordDTO forgetPasswordDTO)
        {

            //var User = _uow.UserRepository.GetById(forgetPasswordDTO.UserId);
            //if (User != null)
            //{
            //    User.Password = EncryptANDDecrypt.EncryptText(forgetPasswordDTO.NewPassword);
            //    _uow.UserRepository.Update(User);
            //    _uow.Save();

            //    return Ok(new { MessageEng = "Password Changed ..." });
            //}
            return BadRequest("Wrong UserId");
        }
        [AllowAnonymous]
        [HttpGet, Route("ActivateAccountOTP")]
        public async Task<IActionResult> ActivateViaCode(int getCode)
        {
            //VerifyCodeHelper VCode = new VerifyCodeHelper(_uow, _SMS, _mailService);
            //var GtResult = VCode.ActivateOTP(getCode);

            //if (GtResult)
            //{

            //    return Ok(GtResult);
            //}

            return BadRequest("Activation Error ... ");

        }
        [AllowAnonymous]
        [HttpPost, Route("ActivateEmailAccount")]
        public async Task<IActionResult> ActivateAccount(string Phone)
        {
            try
            {
                //var User = _uow.UserRepository.GetAll().Where(a => a.Phone == Phone).FirstOrDefault();
                //User.IsActive = true;
                //_uow.UserRepository.Update(User);
                //_uow.Save();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest("Internal server Error" + "" + ex.Message);
            }
        }



        // 3. Get User
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetWajbaUser(int id)
        {
            // return Ok(result);

            if (ModelState.IsValid)
            {
                try
                {


                    var result = await _WajbaUsersAppService.AccountInfoGetByWajbaUserId(id);

                    return Ok(new ApiResponse<GetUserDto>
                    {
                        Success = true,
                        Message = "User Retrived successfully.",
                        Data = result
                    });

                }
                catch (Exception ex)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Error Retrived User: {ex.Message}",
                        Data = null
                    });
                    //  return BadRequest(" Internal server error" + " " + ex.Message);

                }

            }
            return BadRequest(new { MessageAr = "مستخدم غير موجود", MessageEng = "Invalid User !" });

        }

        //[HttpGet("getby token")]
        //public async Task<ActionResult> Getuserbytoken(string token)
        //{
        //	return Ok(await _WajbaUsersAppService.Decodetoken(token));
        //}

        // 4. Get User List
        [HttpGet("listWajbaUser")]
        public async Task<ActionResult<PagedResultDto<WajbaUserDto>>> GetWajbaUser([FromQuery] GetUserListDto input)
        {
            var result = await _WajbaUsersAppService.GetUserListAsync(input);
            return Ok(result);
        }

        //// 5. Delete User
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWajbaUser(int id)
        //{
        //    await _WajbaUsersAppService.DeleteUserAsync(id);
        //    return Ok();
        //}
    }
}
