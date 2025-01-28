using Wajba.Dtos.CustomerContract;
using Wajba.Dtos.UserAddressContract;
using Wajba.Dtos.WajbaUsersContract;
using Wajba.Models.WajbaUserDomain;

namespace Wajba.Mapping;

public class WajbaUserAddressMapping : Profile
{
    public WajbaUserAddressMapping()
    {
        CreateMap<WajbaUserAddress, CreateUserAddressDto>();
        CreateMap<WajbaUserAddress, UpdateUserAddressDto>();
        CreateMap<WajbaUserAddress, UserAddressDto> ();
        CreateMap<WajbaUserAddress, GetUserDto>();



    }
}
