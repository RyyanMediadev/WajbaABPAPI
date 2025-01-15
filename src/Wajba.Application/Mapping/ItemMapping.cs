namespace Wajba.Mapping;

public class ItemMapping : Profile
{
    public ItemMapping()
    {
        CreateMap<Item, ItemDto>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.Category.Id))
                .ForMember(x => x.ItemType, opt => opt.MapFrom(x => (int)x.ItemType)).
                ForMember(p => p.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(p => p.imageUrl, opt => opt.MapFrom(x => x.ImageUrl))
                .ForMember(p => p.IsFeatured, opt => opt.MapFrom(x => x.IsFeatured))
                .ForMember(p => p.TaxValue, opt => opt.MapFrom(x => x.TaxValue))
                .ForMember(p => p.Price, opt => opt.MapFrom(x => x.Price))
                .ForMember(p => p.Note, opt => opt.MapFrom(x => x.Note))
                .ForMember(p => p.status, opt => opt.MapFrom(x => x.Status))
                .ForMember(p => p.IsDeleted, opt => opt.MapFrom(x => x.IsDeleted))
                .ForMember(p => p.Branchesids, opt => opt.Ignore())
                .ReverseMap();
    }
}