namespace Wajba.Mapping;

public class CurrencyMapping:Profile
{
    public CurrencyMapping()
    {
        CreateMap<Currencies,CurrenciesDto>()
            .ForMember(c => c.Id, opt => opt.MapFrom(c => c.Id))
            .ForMember(c => c.Name, opt => opt.MapFrom(c => c.Name))
            .ForMember(c => c.Code, opt => opt.MapFrom(c => c.Code))
            .ForMember(c => c.Symbol, opt => opt.MapFrom(c => c.Symbol))
            .ForMember(c => c.IsCryptoCurrency, opt => opt.MapFrom(c => c.IsCryptoCurrency))
            .ReverseMap();
        CreateMap<CreateUpdateCurrenciesDto, Currencies>().
            ForMember(c => c.Id, opt => opt.Ignore())
            .ForMember(c => c.Name, opt => opt.MapFrom(c => c.Name))
            .ForMember(c => c.Code, opt => opt.MapFrom(c => c.Code))
            .ForMember(c => c.Symbol, opt => opt.MapFrom(c => c.Symbol))
            .ForMember(c => c.IsCryptoCurrency, opt => opt.MapFrom(c => c.IsCryptoCurrency));
    }
}
