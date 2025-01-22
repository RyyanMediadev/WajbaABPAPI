using AutoMapper.Internal.Mappers;
using CloudinaryDotNet;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
using Wajba.UserAppService;
using Wajba.UserManagment;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using static Volo.Abp.UI.Navigation.DefaultMenuNames.Application;


namespace Wajba.WajbaUsersService
{
    [RemoteService(false)]
    public class WajbaUsersAppservice : ApplicationService
    {
        private readonly IRepository<WajbaUser, int> _WajbaUserRepository;

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
            /*ICheckUniqes checkUniqes, IPasswordHasher passwordHasher*/)
        {
            //_passwordHasher = (IPasswordHasher<WajbaUser>?)passwordHasher;
            //_objectMapper = objectMapper;

            _WajbaUserRepository = WajbaUserRepository;
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

        public WajbaUser IsValidUser(string Mobile, string password)
        {
            var user = _WajbaUserRepository.FirstOrDefaultAsync(ent => ent.Phone.ToLower() == Mobile.ToLower().Trim()
            && ent.Password == EncryptANDDecrypt.EncryptText(password)).Result;
            return user != null ? user : null;
        }




        public WajbaUser AuthenticateUser(LogInDto request, out string token)
        {

            token = string.Empty;
            var user = IsValidUser(request.Phone, request.Password);

            if (user != null)
            {
                var GetUserpers = _WajbaUserRepository.FirstOrDefaultAsync(a => a.Id == user.Id).Result.Type;
                List<Claim> ClaimList = new List<Claim>();


                //var profiletype = _unitOfWork.ProfileRepository.GetMany(a => a.Id == item.ProfileId).FirstOrDefault();
                // string profiletype = GetUserpers.Type.ToString();
                ClaimList.Add(new Claim(ClaimTypes.Role, GetUserpers.ToString()));


                ClaimList.Add(new Claim(ClaimTypes.Name, request.Phone));
                ClaimList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                TokenManagement _tokenManagement = new TokenManagement();

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
                Type = createuserdto.Type,
                status = Status.Active,
                Password = EncryptANDDecrypt.EncryptText(createuserdto.Password),
            };


            WajbaUser WajbaUser = await _WajbaUserRepository.InsertAsync(wajbaUser, true);

            #region ApplyRoles

            if (createuserdto.Type == UserTypes.Admin)
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
            if (createuserdto.Type == UserTypes.Employee)
            {
                //DeliveryboyProfile

                //var getPermission = _uow.ProfileRepository.GetMany(a => a.NameEn == RoleConstant.StaffMemberProfile).FirstOrDefault();

                //_uow.UserProfileRepository.Add(new UserProfile { ProfileId = getPermission.Id, UserId = User.Id });




                //_uow.Save();




            }

            if (createuserdto.Type == UserTypes.Deliveryboy)
            {
                //DeliveryboyProfile
                //var getPermission = _uow.ProfileRepository.GetMany(a => a.NameEn == RoleConstant.StaffMemberProfile).FirstOrDefault();

                //_uow.UserProfileRepository.Add(new UserProfile { ProfileId = getPermission.Id, UserId = User.Id });


            }


            if (createuserdto.Type == UserTypes.Customer)
            {
                //CustomerProfile

                //var getPermission = _uow.ProfileRepository.GetMany(a => a.NameEn == RoleConstant.StaffMemberProfile).FirstOrDefault();

                //_uow.UserProfileRepository.Add(new UserProfile { ProfileId = getPermission.Id, UserId = User.Id });



            }


            #endregion

            return ObjectMapper.Map<WajbaUser, CreateUserDto>(WajbaUser);
        }


        public async Task UpdateUserAsync(UpdateWajbaUserDto input)
        {
            var user = await _WajbaUserRepository.FirstOrDefaultAsync(u => u.Id == input.Id);
            user.FullName = input.FullName;
            user.Email = input.Email;
            user.Phone = input.Phone;
            user.status = (Status)input.Status;
            user.Type = (UserTypes)input.Type;

            await _WajbaUserRepository.UpdateAsync(user);


        }

        // 3. Get User
        public async Task<GetUserDto> GetUserAsync(int id)
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
    }
}
