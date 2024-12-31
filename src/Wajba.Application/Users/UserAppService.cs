
using BLayer.Security;
using System.Web.Http;
using Volo.Abp.Users;
using Wajba.Dtos.UserDTO;
using Wajba.Models.UsersDomain;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace Wajba.UserAppService;
[RemoteService(false)]

public class UserService : ApplicationService
{
    private readonly IRepository<Models.UsersDomain.User, int> _userRepository;
    public UserService(IRepository<Models.UsersDomain.User, int> userrepository1)
    {
        _userRepository = userrepository1;
    }


    [HttpPost, Route("Register")]
    public async Task<UserInfoDTO> Register(UserInfoDTO userDto)
    {
        Models.UsersDomain.User user = new Models.UsersDomain.User()
        {
            FirstName = userDto.FirstName,
            SecoundName = userDto.SecoundName,
            Phone = userDto.Phone,
            Email = userDto.Email,
            Password = EncryptANDDecrypt.EncryptText(userDto.Password),
            Address = userDto.Address,
            ProfileId = userDto.ProfileId

        };
        Models.UsersDomain.User user1 = await _userRepository.InsertAsync(user, true);
        return ObjectMapper.Map<Models.UsersDomain.User, UserInfoDTO>(user1);

    }
}
