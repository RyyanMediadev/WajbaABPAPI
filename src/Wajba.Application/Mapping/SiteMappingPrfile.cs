namespace Wajba.Mapping;

public class SiteMappingPrfile:Profile
{
    public SiteMappingPrfile()
    {
        CreateMap<Site, SiteDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
            .ForMember(x => x.Quantity, opt => opt.MapFrom(x => x.Quantity))
            .ForMember(x => x.IOSAPPLink, opt => opt.MapFrom(x => x.IOSAPPLink))
            .ForMember(x => x.AndroidAPPLink, opt => opt.MapFrom(x => x.AndroidAPPLink))
            .ForMember(x => x.GoogleMapKey, opt => opt.MapFrom(x => x.GoogleMapKey))
            .ForMember(x => x.CurrencyPosition, opt => opt.MapFrom(x => x.currencyPosition))
            .ForMember(x => x.LanguageSwitch, opt => opt.MapFrom(x => x.languageSwitch))
            .ForMember(x => x.BranchId, opt => opt.MapFrom(x => x.BranchId))
            .ForMember(x => x.CurrencyId, opt => opt.MapFrom(x => x.CurrencyId))
            .ForMember(x => x.LanguageId, opt => opt.MapFrom(x => x.LanguageId))
            .ForMember(p => p.Copyrights, opt => opt.MapFrom(x => x.Copyrights))
            .ReverseMap();
        CreateMap<CreateSiteDto, Site>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
            .ForMember(x => x.Quantity, opt => opt.MapFrom(x => x.digitAfterDecimal))
            .ForMember(x => x.IOSAPPLink, opt => opt.MapFrom(x => x.iosappLink))
            .ForMember(x => x.AndroidAPPLink, opt => opt.MapFrom(x => x.androidAPPLink))
            .ForMember(x => x.GoogleMapKey, opt => opt.MapFrom(x => x.googleMapKey))
            .ForMember(x => x.currencyPosition, opt => opt.MapFrom(x => x.currencyPosition))
            .ForMember(x => x.languageSwitch, opt => opt.MapFrom(x => x.languageSwitch))
            .ForMember(x => x.BranchId, opt => opt.MapFrom(x => x.defaultBranch))
            .ForMember(x => x.CurrencyId, opt => opt.MapFrom(x => x.defaultCurrency))
            .ForMember(x => x.LanguageId, opt => opt.MapFrom(x => x.defaultLanguage))
            .ForMember(p => p.Copyrights, opt => opt.MapFrom(x => x.Copyrights)).
            ReverseMap();
    }
}
