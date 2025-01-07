namespace Wajba.Mapping;

public class Languagemapping: Profile
{
    public Languagemapping()
    {
        CreateMap<Language, LanguageDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Code, opt => opt.MapFrom(x => x.Code))
            .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
            .ForMember(p => p.Image, opt => opt.MapFrom(x => x.ImageUrl))
            .ReverseMap();
        CreateMap<CreateUpdateLanguageDto, Language>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Code, opt => opt.MapFrom(x => x.Code))
            .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
            .ForMember(p => p.ImageUrl, opt => opt.Ignore())
       .ReverseMap();
    }
}