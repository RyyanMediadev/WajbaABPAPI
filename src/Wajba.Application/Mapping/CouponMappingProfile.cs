namespace Wajba.Mapping
{
    public class CouponMappingProfile : Profile
    {
        public CouponMappingProfile()
        {
            CreateMap<Coupon, CouponDto>()
                .ForMember(p => p.DiscountType, p => p.Ignore());
            CreateMap<CreateUpdateCouponDto, Coupon>()
                .ForMember(p => p.DiscountType, p => p.Ignore());
        }
    }
}