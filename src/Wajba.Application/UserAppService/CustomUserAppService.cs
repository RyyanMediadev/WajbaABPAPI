using Microsoft.AspNetCore.Identity;
using Wajba.Dtos.CustomerContract;
using Wajba.Models.UsersDomain;
using IObjectMapper = Volo.Abp.ObjectMapping.IObjectMapper;


namespace Wajba.CustomerAppService
{
    [RemoteService(false)]
    public class CustomUserAppService : ApplicationService
    {
        private readonly UserManager<APPUser> _userManager;
        private readonly IPasswordHasher<APPUser> _passwordHasher;
        private readonly IRepository<APPUser> _userRepository;
        private readonly IObjectMapper _objectMapper;
        public CustomUserAppService(UserManager<APPUser> userManager, IObjectMapper objectMapper, IPasswordHasher<APPUser> passwordHasher, IRepository<APPUser> userRepository)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _objectMapper = objectMapper;
        }

        public async Task CreateuserAsync(CreateUserDto input)
        {
            if (input.Password != input.ConfirmPassword)
            {
                throw new UserFriendlyException("Passwords do not match.");
            }
            var user = new APPUser
            {
                UserName = input.Email,
                Email = input.Email,
                PhoneNumber = input.Phone,
                FullName = input.FullName,
                Type = (UserTypes)input.Type,
                status = Status.Active,
                PasswordHash = _passwordHasher.HashPassword(null, input.Password)
            };
            // Assign role based on user type
            if (input.Type == (int)UserTypes.Admin)
                await _userManager.AddToRoleAsync(user, "Administrator");
            else if (input.Type ==(int) UserTypes.Customer)
                await _userManager.AddToRoleAsync(user, "Customer");
            else if (input.Type == (int)UserTypes.Employee)
                await _userManager.AddToRoleAsync(user, "Employee");
            else if (input.Type ==(int) UserTypes.Deliveryboy)
                await _userManager.AddToRoleAsync(user, "DeliveryBoy");
            var hashedPassword = _passwordHasher.HashPassword(user, input.Password);
            user.PasswordHash = hashedPassword;

            //await _userRepository.InsertAsync(user);
            await _userManager.CreateAsync(user);
        }

        public async Task UpdateUserAsync(UpdateUserDto input)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Id == input.Id);
            user.FullName = input.FullName;
            user.Email = input.Email;
            user.PhoneNumber = input.Phone;
            user.status = (Status)input.Status;
            user.Type = (UserTypes)input.Type;

            await _userRepository.UpdateAsync(user);
        }

        // 3. Get User
        public async Task<GetUserDto> GetUserAsync(Guid id)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Id == id);
            return _objectMapper.Map<APPUser, GetUserDto>(user);
        }

        // 4. Get User List
        public async Task<PagedResultDto<GetUserDto>> GetUserListAsync(GetUserListDto input)
        {
            var query = await _userRepository.GetQueryableAsync();  // This gives you IQueryable<APPUser>

            // Apply filters
            if (!string.IsNullOrEmpty(input.FullName))
            {
                query = query.Where(u => u.FullName.Contains(input.FullName));
            }

            if (input.Type.HasValue)
            {
                query = query.Where(u => u.Type ==(UserTypes) input.Type);
            }

            if (input.Status.HasValue)
            {
                query = query.Where(u => u.status ==(Status) input.Status);
            }

            // Apply pagination (Skip and Take)
            var totalCount = await query.CountAsync(); // Get total count for pagination
            var users = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync(); // Get paginated users
                                                                                                    // Map the list of users (use 'users' instead of 'user')
            var userDtos = users.Select(user => new GetUserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Type = (int)user.Type,
                Status = (int)user.status

            }).ToList();

            // Return the result in PagedResultDto
            return new PagedResultDto<GetUserDto>(totalCount, userDtos);
        }

        // 5. Delete User
        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Id == id);
            await _userRepository.DeleteAsync(user);
        }
    }
}
