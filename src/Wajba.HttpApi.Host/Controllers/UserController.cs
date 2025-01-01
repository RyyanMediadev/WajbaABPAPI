<<<<<<< HEAD
﻿global using Microsoft.AspNetCore.Mvc;
global using Volo.Abp.Application.Dtos;
global using Volo.Abp.Domain.Entities;
global using Wajba.APIResponse;
global using Wajba.Categories;
global using Wajba.Dtos.Categories;
using System.Net;
using Wajba.Dtos.UserDTO;
using Wajba.UserAppService;
using Wajba.Users;
=======
﻿global using Wajba.Dtos.SitesContact;
global using Wajba.SiteService;
using Wajba.Dtos.UserDTO;
using Wajba.UserAppService;

>>>>>>> 224daba17be966aee697fc65ecabf2cf065bca85

namespace Wajba.Controllers;


public class UserController : WajbaController
{
    private readonly UserService _UserService;

    public UserController(UserService userAppService)
    {
        _UserService = userAppService;
    }

<<<<<<< HEAD
    [HttpPost]
=======
     [HttpPost]
>>>>>>> 224daba17be966aee697fc65ecabf2cf065bca85
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
<<<<<<< HEAD
=======


    //[AllowAnonymous]
     [HttpPost,Route("LogIn")]
    public IActionResult LogIn(LogInDto LogInDto)
    {
        try
        {
            //if (!ModelState.IsValid)
            //{ return BadRequest(ModelState); }

            ////          public User IsValidUser(string Mobile, string password)
            ////{
            ////    var user = _uow.UserRepository.GetMany(ent => ent.Phone.ToLower() == Mobile.ToLower().Trim()
            ////    && ent.Password == EncryptANDDecrypt.EncryptText(password)
            ////    && ent.Password == EncryptANDDecrypt.EncryptText(password)
            ////    && ent.Password == EncryptANDDecrypt.EncryptText(password)).ToHashSet();
            ////    return user.Count() == 1 ? user.FirstOrDefault() : null;
            ////}
            ////var user = _authService.AuthenticateUser(LogInDto, out string token);

            ////var user = _authService.AuthenticateUser(LogInDto, out string token);
            //if (user == null)
            //{
            //    return BadRequest(new { MessageAr = "! خطأ في كلمة المرور او رقم الجوال", MessageEng = "Incorrect Email or Password  !" });
            //}
            //if (!user.IsActive)
            //{
            //    return BadRequest(new { MessageAr = " !الحساب غير مفعل توجه لبريدك الالكتروني للتفعيل", MessageEng = "Account is not Active ;Check Your E-mail to Activate !" });
            //}
            //if (user != null)
            //{
            //    //Implement User Profiles

            //    user.Password = null;
            //    return Ok(new
            //    {
            //        user,
            //        token
            //    });
            //}
            return BadRequest(new { MessageAr = "خطأ في كلمة المرور او رقم الجوال", MessageEng = "Account is not Active ; Check Your E-mail to Activate" });

        }
        catch (Exception ex)
        {
            return BadRequest(new { MessageAr = ex.Message, MessageEng = ex.Message });

        }
    }




>>>>>>> 224daba17be966aee697fc65ecabf2cf065bca85
}




