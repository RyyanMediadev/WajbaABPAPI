namespace Wajba.Mapping;

public class FaqMapping:Profile
{
    public FaqMapping()
    {
        CreateMap<FAQs, FaqDto>()
            .ForMember(p => p.Question, opt => opt.MapFrom(p => p.Question))
            .ForMember(p => p.Answer, opt => opt.MapFrom(p => p.Answer))
            .ForMember(p => p.Id, opt => opt.MapFrom(p => p.Id));
        CreateMap<CreateFaqs, FAQs>()
            .ForMember(p => p.Question, opt => opt.MapFrom(p => p.Question))
            .ForMember(p => p.Answer, opt => opt.MapFrom(p => p.Answer));

    }
}
