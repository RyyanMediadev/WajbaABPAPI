using AutoMapper.Internal.Mappers;
using CloudinaryDotNet;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Wajba.CustomIdentity;
using Wajba.Dtos.UserDTO;
using Wajba.Dtos.WajbaUsersContract;
using Wajba.Models.FaqsDomain;
using Wajba.Models.UsersDomain;
using Wajba.Models.WajbaUserDomain;
using Wajba.Services.ImageService;
using Wajba.SharedTokenManagement;
using Wajba.UserAppService;
using Wajba.UserManagment;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using static Volo.Abp.UI.Navigation.DefaultMenuNames.Application;
using TokenManagement = Wajba.SharedTokenManagement.TokenManagement;



namespace Wajba.WajbaUsersService
{
    [RemoteService(false)]
    public class WajbaUsersAppservice : ApplicationService
    {
        private readonly IRepository<WajbaUser, int> _WajbaUserRepository;
        private readonly TokenManagement _tokenManagement;
        //private readonly VerifyCodeHelper _VerifyCodeHelper;
        //private readonly IPasswordHasher<WajbaUser> _passwordHasher;
        //private readonly IObjectMapper _objectMapper;
        //private readonly IUnitOfWork _uow;
        //private readonly ISmsSenderService _SMS;
        //private readonly IAuthenticateService _authService;
        //private readonly ICheckUniqes _checkUniq;
        ////   private readonly IHostingEnvironment _hostingEnvironment;
        //private readonly IMapper _mapper;
        //private readonly IUserManagementService _UserManagementService;
        //private readonly IMailService _mailService;
        public WajbaUsersAppservice(/*IObjectMapper objectMapper,  */
            IRepository<WajbaUser, int> WajbaUserRepository
            , IOptions<TokenManagement> tokenManagement)
        {
            //_passwordHasher = (IPasswordHasher<WajbaUser>?)passwordHasher;
            //_objectMapper = objectMapper;

            _WajbaUserRepository = WajbaUserRepository;
            _tokenManagement = tokenManagement.Value;
            //_VerifyCodeHelper = VerifyCodeHelper;

            //_checkUniq = checkUniqes;
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

        //public WajbaUser IsValidUser(string Mobile, string password)
        //{
        //    var user = _WajbaUserRepository.FirstOrDefaultAsync(ent => ent.Phone.ToLower() == Mobile.ToLower().Trim()
        //    && ent.Password == EncryptANDDecrypt.EncryptText(password));
        //    return user != null ? user : null;
        //}

        public async Task<WajbaUser> IsValidUserAsync(LogInWajbaUserDto request)
        {

            if(request.LogInAPPCode=="EndUSerApp@SpotIdeas")
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(request.Phone))
                {
                    return null;
                }

                // Find the user based on mobile and encrypted password
                //var encryptedPassword = EncryptANDDecrypt.EncryptText(password);
                var user = await _WajbaUserRepository.FirstOrDefaultAsync(
                    ent => ent.Phone.ToLower() == request.Phone);

                return user;

            }

            if (request.LogInAPPCode == "DashBoardweb@SpotIdeas")
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(request.Email) && string.IsNullOrWhiteSpace(request.Password))
                {
                    return null;
                }

                // Find the user based on mobile and encrypted password
                var encryptedPassword = EncryptANDDecrypt.EncryptText(request.Password);
                var user = await _WajbaUserRepository.FirstOrDefaultAsync(
                    ent => ent.Email.ToLower() == request.Email &&ent.Password==encryptedPassword);

                return user;

            }
            return null;
        }

        public async Task<WajbaUser> ValidateOtpUser(string Phone)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Phone))
            {
                return null;
            }

            // Find the user based on mobile and encrypted password
        //    var encryptedPassword = EncryptANDDecrypt.EncryptText(password);
            var user = await _WajbaUserRepository.FirstOrDefaultAsync(
                ent => ent.Phone.ToLower() == Phone);

            return user;
        }


        public bool SendOTP(string MobileNum, int userId, string Email)
        {
            if (MobileNum != null && userId != 0)
            {
                int num = new Random().Next(1000, 9999);
                //   var VC = new VerfiyCode { Date = DateTime.Now.AddMinutes(5), PhoneNumber = MobileNum, UserId = userId, VirfeyCode = num };
                //_uow.VerifyCodeRepository.Add(VC);
                //_uow.Save();
                var Message = string.Format("Use This OTP Code {0}\n " + "كود التحقق \n {1}", num.ToString(), num.ToString());
               //// var s = _SMS.SendSMS(MobileNum, Message);
                if (Email != null)
                {
                   // var email = _mailService.SendActivateEmailAsync(new WelcomeRequest { ToEmail = Email, UserName = Email, Id = userId, Link = num.ToString() });

                }


                return true;

            }
            return false;

        }
        public bool ActivateOTP(int VerfiyCode)
        {
            if (VerfiyCode == 1234)
            {
                return true;
            }
            // var Entity = _uow.VerifyCodeRepository.GetMany(a => a.VirfeyCode == VerfiyCode).FirstOrDefault();
            //if (Entity != null)
            //{
            //    if (Entity.Date < DateTime.Now)
            //    {
            //        return false;
            //    }
            //    //var User = _uow.UserRepository.GetById(Entity.UserId);
            //    //User.IsActive = true;
            //    //_uow.UserRepository.Update(User);
            //    //_uow.VerifyCodeRepository.Delete(Entity.Id);

            //    //_uow.Save();

            //    return true;

            //}
            return false;
        }

        public class VerifyCodeHelper
        {
            private readonly IUnitOfWork _uow;
            public readonly ISmsSenderService _SMS;
            private readonly IMailService _mailService;

            public VerifyCodeHelper(IUnitOfWork uow, ISmsSenderService SMS, IMailService _mailService)
            {
                _uow = uow;
                _SMS = SMS;
                this._mailService = _mailService;

            }
            public bool SendOTP(string MobileNum, int userId, string Email)
            {
                if (MobileNum != null && userId != 0)
                {
                    int num = new Random().Next(1000, 9999);
                    //   var VC = new VerfiyCode { Date = DateTime.Now.AddMinutes(5), PhoneNumber = MobileNum, UserId = userId, VirfeyCode = num };
                    //_uow.VerifyCodeRepository.Add(VC);
                    //_uow.Save();
                    var Message = string.Format("Use This OTP Code {0}\n " + "كود التحقق \n {1}", num.ToString(), num.ToString());
                    var s = _SMS.SendSMS(MobileNum, Message);
                    if (Email != null)
                    {
                        var email = _mailService.SendActivateEmailAsync(new WelcomeRequest { ToEmail = Email, UserName = Email, Id = userId, Link = num.ToString() });

                    }


                    return true;

                }
                return false;

            }
            public bool ActivateOTP(int VerfiyCode)
            {
                if (VerfiyCode == 1234)
                {
                    return true;
                }
                // var Entity = _uow.VerifyCodeRepository.GetMany(a => a.VirfeyCode == VerfiyCode).FirstOrDefault();
                //if (Entity != null)
                //{
                //    if (Entity.Date < DateTime.Now)
                //    {
                //        return false;
                //    }
                //    //var User = _uow.UserRepository.GetById(Entity.UserId);
                //    //User.IsActive = true;
                //    //_uow.UserRepository.Update(User);
                //    //_uow.VerifyCodeRepository.Delete(Entity.Id);

                //    //_uow.Save();

                //    return true;

                //}
                return false;
            }
            //  public bool ForgetPasswordOTP(int VerfiyCode)
            //{
            //    var Entity = _uow.VerifyCodeRepository.GetMany(a => a.VirfeyCode == VerfiyCode).FirstOrDefault();
            //    if (Entity != null)
            //    {
            //        if (Entity.Date < DateTime.Now)
            //        {
            //            return false;
            //        }

            //        _uow.VerifyCodeRepository.Delete(Entity.Id);
            //        _uow.Save();

            //        return true;

            //    }
            //    return false;
            //  }
        }




        public async Task<WajbaUser> AuthenticateUser(LogInWajbaUserDto request)
        {

            //token = string.Empty;


            var user = await IsValidUserAsync(request);

            //if (user != null)
            //{
            //    var GetUserpers = _WajbaUserRepository.FirstOrDefaultAsync(a => a.Id == user.Id).Result.Type;
            //    List<Claim> ClaimList = new List<Claim>();


            //    //var profiletype = _unitOfWork.ProfileRepository.GetMany(a => a.Id == item.ProfileId).FirstOrDefault();
            //    // string profiletype = GetUserpers.Type.ToString();
            //    ClaimList.Add(new Claim(ClaimTypes.Role, GetUserpers.ToString()));


            //    ClaimList.Add(new Claim(ClaimTypes.Name, request.Phone));
            //    ClaimList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            //    TokenManagement _tokenManagement = new TokenManagement();

            //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            //    var expireDate = DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration);

            //    var tokenDiscriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(ClaimList),
            //        Expires = expireDate,
            //        SigningCredentials = credentials
            //    };
            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    var tokenObj = tokenHandler.CreateToken(tokenDiscriptor);
            //    token = tokenHandler.WriteToken(tokenObj);
            //}
            return user;

        }

         public async Task<string> GenerateTokenAsync(WajbaUser WajbaUser)
        {
            if (WajbaUser != null)
            {
                List<Claim> ClaimList = new List<Claim>();
                ClaimList.Add(new Claim(ClaimTypes.Role, WajbaUser.Id.ToString()));
                ClaimList.Add(new Claim(ClaimTypes.Name, WajbaUser.Phone));
                ClaimList.Add(new Claim(ClaimTypes.NameIdentifier, WajbaUser.Id.ToString()));
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var expireDate = DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration);
                var tokenDiscriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(ClaimList),
                    Expires = expireDate,
                    SigningCredentials = credentials,
                    Issuer=_tokenManagement.Issuer,
                    Audience=_tokenManagement.Audience,
                    
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenObj = tokenHandler.CreateToken(tokenDiscriptor);
                string token = tokenHandler.WriteToken(tokenObj);
                return token;
            }
            return null;

        }
        public  ClaimsPrincipal Decodetoken(string token)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var validateparams = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret)),
                ValidateAudience = true,
                ValidateIssuer=true,
                ValidIssuer=_tokenManagement.Issuer,
                ValidAudience = _tokenManagement.Audience,
                ValidateLifetime = true
            };
            try
            {
                var principle = tokenhandler.ValidateToken(token, validateparams, out var validatedToken);
                return principle;
            }
            catch(Exception ex)
            {
                return null;
            }
            //var expireDate = DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration);

            //return tokenhandler.ValidateToken(token, new TokenValidationParameters()
            //{
            //});

            //var jwttoken = tokenhandler.ReadJwtToken(token);
            //if (jwttoken == null)
            //    throw new Exception("Invalid token");
            //var usercliam = jwttoken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier);
            //return await _WajbaUserRepository.FirstOrDefaultAsync(p => p.Id == int.Parse(usercliam.Value));
        }
        public async Task<WajbaUser> GetWajbaUserbytoken(string token)
        {
            ClaimsPrincipal claims = Decodetoken(token);
            if (claims == null)
                return null;
            var userid = claims.FindFirst(ClaimTypes.NameIdentifier);
            if (userid == null)
                return null;
            var id = userid.Value;
            return await _WajbaUserRepository.FirstOrDefaultAsync(p => p.Id == int.Parse(id));
        }
        public int? getUserId(string Phone, string Email)

        {
            // Get user id by name 
            WajbaUser user = _WajbaUserRepository.FirstOrDefaultAsync(ent => ent.Phone.ToLower() == Phone.ToLower().Trim()
            || ent.Email.ToLower() == Email.ToLower().Trim()).Result;
            return user != null ? user.Id : null;
        }
        public async Task<CreateUserDto> Register(CreateUserDto createuserdto)
        {

            //var MessageArEng = CheckUniqeValue(new UniqeDTO { Email = createuserdto.Email, Mobile = createuserdto.Phone });
            //if (MessageArEng.Count() > 0)
            //{
            //    throw new UserFriendlyException("Exists Email or Phone" + MessageArEng);

            //}


            if (createuserdto.Password != createuserdto.ConfirmPassword)
            {
                throw new UserFriendlyException("Passwords do not match.");
            }
            WajbaUser wajbaUser = new WajbaUser
            {
                Email = createuserdto.Email,
                Phone = createuserdto.Phone,
                FullName = createuserdto.FullName,
                Type = (UserTypes)createuserdto.Type,
                status = Status.Active,
                GenderType = (GenderType)createuserdto.GenderType,

                Password = EncryptANDDecrypt.EncryptText(createuserdto.Password),
            };


            WajbaUser WajbaUser = await _WajbaUserRepository.InsertAsync(wajbaUser, true);

            #region ApplyRoles

            if (createuserdto.Type ==1/* UserTypes.Admin*/)
            {
                //AdminProfile

                Type t = typeof(RoleConstant);
                FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);


                for (int i = 0; i < fields.Length; i++)
                {

                    //_uow.UserProfileRepository.Add(new UserProfile { ProfileId = i + 1, UserId = User.Id });

                    //_uow.Save();

                }

            }
            if (createuserdto.Type ==2/* UserTypes.Employee*/)
            {
                //DeliveryboyProfile

                //var getPermission = _uow.ProfileRepository.GetMany(a => a.NameEn == RoleConstant.StaffMemberProfile).FirstOrDefault();

                //_uow.UserProfileRepository.Add(new UserProfile { ProfileId = getPermission.Id, UserId = User.Id });




                //_uow.Save();




            }

            if (createuserdto.Type ==3 /*UserTypes.Deliveryboy*/)
            {
                //DeliveryboyProfile
                //var getPermission = _uow.ProfileRepository.GetMany(a => a.NameEn == RoleConstant.StaffMemberProfile).FirstOrDefault();

                //_uow.UserProfileRepository.Add(new UserProfile { ProfileId = getPermission.Id, UserId = User.Id });


            }


            if (createuserdto.Type ==4/* UserTypes.Customer*/)
            {
                //CustomerProfile

                //var getPermission = _uow.ProfileRepository.GetMany(a => a.NameEn == RoleConstant.StaffMemberProfile).FirstOrDefault();

                //_uow.UserProfileRepository.Add(new UserProfile { ProfileId = getPermission.Id, UserId = User.Id });



            }


            #endregion

            return ObjectMapper.Map<WajbaUser, CreateUserDto>(WajbaUser);
        }


        public async Task<UpdateWajbaUserDto> UpdateUserAsync(UpdateWajbaUserDto input)
        {
            var user = await _WajbaUserRepository.FirstOrDefaultAsync(u => u.Id == input.Id);
            user.FullName = input.FullName;
            user.Email = input.Email;
            user.Phone = input.Phone;
            user.status = (Status)input.Status;
            user.Type = (UserTypes)input.Type;
            user.GenderType = (GenderType)input.GenderType;
            //user.Password = user.Password;


            WajbaUser ws  =await _WajbaUserRepository.UpdateAsync(user);
            ws.Password = null;

            return ObjectMapper.Map<WajbaUser, UpdateWajbaUserDto>(ws);

        }

        // 3. Get User
        public async Task<GetUserDto> AccountInfoGetByWajbaUserId(int id)
        {
            var user = await _WajbaUserRepository.FirstOrDefaultAsync(u => u.Id == id);
            return ObjectMapper.Map<WajbaUser, GetUserDto>(user);
        }

        // 4. Get User List
        public async Task<PagedResultDto<WajbaUserDto>> GetUserListAsync(GetUserListDto input)
        {
            var query = await _WajbaUserRepository.GetQueryableAsync();  // This gives you IQueryable<APPUser>

            // Apply filters
            if (!string.IsNullOrEmpty(input.FullName))
            {
                query = query.Where(u => u.FullName.Contains(input.FullName));
            }

            if (input.Type.HasValue)
            {
                query = query.Where(u => u.Type == (UserTypes)input.Type);
            }

            if (input.Status.HasValue)
            {
                query = query.Where(u => u.status == (Status)input.Status);
            }

			if (!string.IsNullOrEmpty(input.Email))
			{
				query = query.Where(u => u.Email == input.Email);
			}

			if (!string.IsNullOrEmpty(input.Phone))
			{
				query = query.Where(u => u.Phone == input.Phone);
			}
            if (input.GenderType.HasValue)
            {
                query = query.Where(u => u.GenderType.ToString() == input.GenderType.ToString());
            }


            // Apply pagination (Skip and Take)
            var totalCount = await query.CountAsync(); // Get total count for pagination
            var users = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync(); // Get paginated users
                                                                                                    // Map the list of users (use 'users' instead of 'user')
            var userDtos = users.Select(user => new WajbaUserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Type = (int)user.Type,
                Status = (int)user.status

            }).ToList();

            // Return the result in PagedResultDto
            return new PagedResultDto<WajbaUserDto>(totalCount, userDtos);
        }

        // 5. Delete User
        public async Task DeleteUserAsync(int id)
        {
            var user = await _WajbaUserRepository.FirstOrDefaultAsync(u => u.Id == id);
            await _WajbaUserRepository.DeleteAsync(user);
        }

        public async Task<AccountInfoEditByWajbaUserId> AccountInfoEdit(AccountInfoEditByWajbaUserId AccountInfoEditByWajbaUserId)
        {

            var user = await _WajbaUserRepository.FirstOrDefaultAsync(u => u.Id == AccountInfoEditByWajbaUserId.Id);
            user.FullName = AccountInfoEditByWajbaUserId.FullName;
            user.Email = AccountInfoEditByWajbaUserId.Email;
            user.Phone = AccountInfoEditByWajbaUserId.Phone;
            user.status = (Status)AccountInfoEditByWajbaUserId.Status;
            user.Type = (UserTypes)AccountInfoEditByWajbaUserId.Type;
            user.GenderType = (GenderType)AccountInfoEditByWajbaUserId.GenderType;
            //user.Password = user.Password;


            WajbaUser ws = await _WajbaUserRepository.UpdateAsync(user);
            //ws.Password = null;

            return ObjectMapper.Map<WajbaUser, AccountInfoEditByWajbaUserId>(ws);
        }
    }
}
