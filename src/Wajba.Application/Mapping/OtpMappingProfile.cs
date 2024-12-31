namespace Wajba.Mapping;

public class OtpMappingProfile:Profile
{
    public OtpMappingProfile()
    {
        CreateMap<OTP, OTPDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.ExpiryTimeInMinutes, opt => opt.MapFrom(x => x.ExpiryTimeInMinutes))
            .ForMember(x => x.DigitLimit, opt => opt.MapFrom(x => x.DigitLimit))
            .ReverseMap();
        CreateMap<CreateUpdateOTPDto, OTP>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.ExpiryTimeInMinutes, opt => opt.MapFrom(x => x.ExpiryTimeInMinutes))
            .ForMember(x => x.DigitLimit, opt => opt.MapFrom(x => x.DigitLimit))
            .ReverseMap();

    }
}
