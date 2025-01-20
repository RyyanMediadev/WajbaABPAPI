using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Web.Http.ModelBinding;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Wajba.CustomIdentity;
using Wajba.Dtos.CustomerContract;
using Wajba.Dtos.UserDTO;
using Wajba.Models.FaqsDomain;
using Wajba.Models.UsersDomain;
using Wajba.Services.ImageService;
using Wajba.UserAppService;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using static Volo.Abp.UI.Navigation.DefaultMenuNames.Application;
using static Wajba.UserAppService.TokenAuthenticationService;
using IObjectMapper = Volo.Abp.ObjectMapping.IObjectMapper;


namespace Wajba.CustomerAppService
{
    [RemoteService(false)]
    public class WajbaUserAppService : ApplicationService
    {
        private readonly IPasswordHasher<WajbaUser> _passwordHasher;
        private readonly IRepository<WajbaUser> _WajbaUserRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWork _uow;
        private readonly ISmsSenderService _SMS;
        private readonly IAuthenticateService _authService;
        private readonly ICheckUniqes _checkUniq;
        //   private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        private readonly IUserManagementService _UserManagementService;
        private readonly IMailService _mailService;
        public WajbaUserAppService(IObjectMapper objectMapper, IPasswordHasher<WajbaUser> passwordHasher, IRepository<WajbaUser> WajbaUserRepository)
        {
            _passwordHasher = passwordHasher;
            _WajbaUserRepository = WajbaUserRepository;
            _objectMapper = objectMapper;
        }





        public async Task<CreateUserDto> Register(CreateUserDto createuserdto)
        {

            var MessageArEng = _checkUniq.CheckUniqeValue(new UniqeDTO { Email = createuserdto.Email, Mobile = createuserdto.Phone });
            if (MessageArEng.Count() > 0)
            {
                throw new UserFriendlyException("Exists Email or Phone" + MessageArEng);

            }


            if (createuserdto.Password != createuserdto.ConfirmPassword)
            {
                throw new UserFriendlyException("Passwords do not match.");
            }
            WajbaUser user = new WajbaUser
            {
                Email = createuserdto.Email,
                Phone = createuserdto.Phone,
                FullName = createuserdto.FullName,
                Type = (UserTypes)createuserdto.Type,
                status = Status.Active,
                Password = _passwordHasher.HashPassword(null, createuserdto.Password)
            };


            WajbaUser WajbaUser = await _WajbaUserRepository.InsertAsync(user, true);

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


        public async Task UpdateUserAsync(UpdateUserDto input)
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
            return _objectMapper.Map<WajbaUser, GetUserDto>(user);
        }

        // 4. Get User List
        public async Task<PagedResultDto<GetUserDto>> GetUserListAsync(GetUserListDto input)
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
