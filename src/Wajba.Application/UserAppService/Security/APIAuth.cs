
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Wajba.Dtos.UserDTO;
using Wajba.UserAppService;
using Wajba.Models.UsersDomain;
using Volo.Abp.Uow;
using static Wajba.UserAppService.TokenAuthenticationService;

namespace Wajba.UserAppService
{

    public interface IAuthenticateService
    {

        WajbaUser AuthenticateUser(LogInDto request, out string token);
    }
    public interface ICheckUniqes
    {
        List<string> CheckUniqeValue(UniqeDTO request);


    }


    public class ChekUniqeSer : ICheckUniqes
    {
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;
        private readonly IRepository<WajbaUser> _WajbaUserRepository;


        public ChekUniqeSer( IUserManagementService service, IOptions<TokenManagement> tokenManagement, IRepository<WajbaUser> wajbaUserRepository)
        {
            _userManagementService = service;
            _tokenManagement = tokenManagement.Value;
            _WajbaUserRepository = wajbaUserRepository;
        }
        public List<string> CheckUniqeValue(UniqeDTO request)
        {



            List<string> MessageArEng = new List<string>();


            var Email = _WajbaUserRepository.FirstOrDefaultAsync(a => a.Email == request.Email);
            var Mobil = _WajbaUserRepository.FirstOrDefaultAsync(a => a.Phone == request.Mobile);

            if (Email != null)
            {
                MessageArEng.Add("Email Already Exist !");
                MessageArEng.Add("البريد الإلكتروني موجود مسبقاً ");

            }

            if (Mobil != null)
            {
                MessageArEng.Add("Mobile Already Exist !");
                MessageArEng.Add("الهاتف موجود مسبقاً");

            }

            return MessageArEng;
        }



    }
    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;
        private readonly IRepository<WajbaUser> _WajbaUserRepository;


        public TokenAuthenticationService(IUnitOfWork unitOfWork, IUserManagementService service, IOptions<TokenManagement> tokenManagement, IRepository<WajbaUser> wajbaUserRepository)
        {
            _userManagementService = service;
            _tokenManagement = tokenManagement.Value;
            _unitOfWork = unitOfWork;
            _WajbaUserRepository = wajbaUserRepository;
        }



        public WajbaUser AuthenticateUser(LogInDto request, out string token)
        {

            token = string.Empty;
            var user = _userManagementService.IsValidUser(request.Phone, request.Password);

            if (user != null)
            {
                var GetUserpers = _WajbaUserRepository.FirstOrDefaultAsync(a => a.Id == user.Id).Result.Type;
                List<Claim> ClaimList = new List<Claim>();
                

                    //var profiletype = _unitOfWork.ProfileRepository.GetMany(a => a.Id == item.ProfileId).FirstOrDefault();

                   // string profiletype = GetUserpers.Type.ToString();
                    ClaimList.Add(new Claim(ClaimTypes.Role, GetUserpers.ToString()));

                
                ClaimList.Add(new Claim(ClaimTypes.Name, request.Phone));
                ClaimList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var expireDate = DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration);

                var tokenDiscriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(ClaimList),
                    Expires = expireDate,
                    SigningCredentials = credentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenObj = tokenHandler.CreateToken(tokenDiscriptor);
                token = tokenHandler.WriteToken(tokenObj);
            }
            return user;

        }

        public interface IUserManagementService
        {
            WajbaUser IsValidUser(string username, string password);
            int? getUserId(string Phone, string Email);
        }

        public class UserManagementService : IUserManagementService
        {
            private readonly IUnitOfWork _uow;
            private readonly IRepository<WajbaUser> _WajbaUserRepository;

            public UserManagementService(IUnitOfWork uow) { _uow = uow; }

            public WajbaUser IsValidUser(string Mobile, string password)
            {
                var user = _WajbaUserRepository.FirstOrDefaultAsync(ent => ent.Phone.ToLower() == Mobile.ToLower().Trim()
                && ent.Password == EncryptANDDecrypt.EncryptText(password)).Result;
                return user!= null ? user: null;
            }

            public int? getUserId(string Phone,string Email)

            {
                // Get user id by name 
                WajbaUser user = _WajbaUserRepository.FirstOrDefaultAsync(ent => ent.Phone.ToLower() == Phone.ToLower().Trim()
                || ent.Email.ToLower() == Email.ToLower().Trim()).Result;
                return user!= null ? user.Id : null;
            }

        }

    }
}
