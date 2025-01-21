using Wajba.Dtos.CustomerContract;
using Wajba.Models.WajbaUserDomain;

namespace Wajba.Mapping;

public class WajbaUserMapping : Profile
{
    public WajbaUserMapping()
    {
        CreateMap<WajbaUser, WajbaUserDto>();
        CreateMap<WajbaUserDto, WajbaUser>();
    }
}
