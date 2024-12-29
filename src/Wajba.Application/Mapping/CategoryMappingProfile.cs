global using AutoMapper;

namespace Wajba.Mapping;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(c => c.ImageUrl, opt => opt.MapFrom(c => c.ImageUrl))
            .ForMember(c => c.Description, opt => opt.MapFrom(c => c.Description))
            .ForMember(c => c.name, opt => opt.MapFrom(c => c.Name))
            .ForMember(c => c.status, opt => opt.MapFrom(c => c.Status))
            .ForMember(c => c.Id, opt => opt.MapFrom(c => c.Id))
            .ReverseMap();

        CreateMap<CreateUpdateCategoryDto, Category>().ForMember(c => c.ImageUrl, opt => opt.Ignore());
    }
}