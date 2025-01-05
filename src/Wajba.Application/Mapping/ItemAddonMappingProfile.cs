global using Wajba.Dtos.ItemAddonContract;
global using Wajba.Models.ItemAddonDomain;

namespace Wajba.Mapping
{
    public class ItemAddonMappingProfile : Profile
    {
        public ItemAddonMappingProfile()
        {
            CreateMap<ItemAddon, ItemAddonDto>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AddonName, opt => opt.MapFrom(src => src.AddonName))
            .ForMember(dest => dest.AdditionalPrice, opt => opt.MapFrom(src => src.AdditionalPrice))
            .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
            .ReverseMap();
            CreateMap<CreateUpdateItemAddonDto, ItemAddon>()
                  .ForMember(ItemAddon => ItemAddon.AddonName, opt => opt.MapFrom(ItemAddon => ItemAddon.AddonName))
            .ForMember(ItemAddon => ItemAddon.AdditionalPrice, opt => opt.MapFrom(ItemAddon => ItemAddon.AdditionalPrice))
            .ForMember(ItemAddon => ItemAddon.ItemId, opt => opt.MapFrom(ItemAddon => ItemAddon.ItemId))
            .ForMember(ItemAddon => ItemAddon.Id, opt => opt.Ignore())
            .ForMember(p=>p.Item, opt => opt.Ignore())
            .ReverseMap();
        }
    }
}
