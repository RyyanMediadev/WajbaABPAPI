global using AutoMapper;
using Nelibur.ObjectMapper;
using System;

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

        //TinyMapper.Bind<Category, CategoryDto>()
        //    .Bind(c => c.ImageUrl, c => c.ImageUrl)
        //    .Bind(c => c.Description, c => c.Description)
        //    .Bind(c => c.name, c => c.Name)
        //    .Bind(c => c.status, c => c.Status)
        //    .Bind(c => c.Id, c => c.Id)
        //    ;

        CreateMap<CreateUpdateCategoryDto, Category>().ForMember(c => c.ImageUrl, opt => opt.Ignore());
    }
}