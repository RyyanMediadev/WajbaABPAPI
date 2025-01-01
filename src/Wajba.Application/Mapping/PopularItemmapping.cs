global using Wajba.Dtos.PopularItemstoday;

namespace Wajba.Mapping;

public class PopularItemmapping:Profile
{
    public PopularItemmapping()
    {
        CreateMap<PopularItem,Popularitemdto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.PrePrice, opt => opt.MapFrom(src => src.PrePrice))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ReverseMap();
    }
}
