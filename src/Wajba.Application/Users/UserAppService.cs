using BLayer.Security;
using Volo.Abp.Domain.Repositories;
using Wajba.Dtos.UserDTO;
using Wajba.Models.UsersDomain;

namespace Wajba.UserAppService;

[RemoteService(false)]
public class UserService : ApplicationService
{
    private readonly IRepository<APPUser, Guid> _userRepository;

    public UserService(IRepository<APPUser, Guid> repository)

    {
        _userRepository = repository;

    }

    public async Task<UserInfoDTO> Register(UserInfoDTO userDto)
    {

        var user = new APPUser()
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password


            //TenantId = userDto.TenantId,
            //UserName = userDto.UserName,
            //userDto.Name
            //      userDto.Surname
            //       userDto.Email
            //      userDto.EmailConfirmed
            //         userDto.PhoneNumber
            //          userDto.PhoneNumberConfirmed
            //     userDto.IsActive
            //     userDto.LockoutEnabled
            //    userDto.AccessFailedCount
            //      userDto.LockoutEnd
            //     userDto.ConcurrencyStamp

            //public int EntityVersion

            //public DateTimeOffset? LastPasswordChangeTime

        };
        APPUser user1 = await _userRepository.InsertAsync(user, true);
        return ObjectMapper.Map<APPUser, UserInfoDTO>(user1);

    }
}
