namespace Wajba.Mapping;

public class OfferMappingProfile:Profile
{
    public OfferMappingProfile()
    {
        CreateMap<Offer, OfferDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
            .ForMember(x => x.BranchId, opt => opt.MapFrom(x => x.BranchId))
            .ForMember(x => x.Status, opt => opt.MapFrom(x => x.status))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate))
            .ForMember(x => x.Image, opt => opt.MapFrom(x => x.ImageUrl))
            .ForMember(p => p.DiscountPercentage, opt => opt.MapFrom(x => x.DiscountPercentage))
            .ForMember(p => p.DiscountType, opt => opt.MapFrom(x => x.discountType))
            .ForMember(p => p.categoryDtos, o => o.Ignore())
            .ForMember(p => p.itemDtos, o => o.Ignore())
            .ReverseMap();
    }
}
