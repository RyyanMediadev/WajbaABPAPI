global using Wajba.Models.CouponsDomain;
global using Wajba.Dtos.CouponContract;

namespace Wajba.CouponService;

[RemoteService(false)]
public class CouponAppService : ApplicationService
{
    private readonly IRepository<Coupon, int> _couponRepository;
    private readonly IImageService _imageService;

    public CouponAppService(IRepository<Coupon, int> couponRepository, IImageService imageService)
    {
        _couponRepository = couponRepository;
        _imageService = imageService;
    }

    // Create Coupon
    public async Task<CouponDto> CreateAsync(CreateUpdateCouponDto input)
    {
        var imageUrl = input.Image != null
            ? await _imageService.UploadAsync(input.Image)
            : null;

        var coupon = ObjectMapper.Map<CreateUpdateCouponDto, Coupon>(input);
        coupon.ImageUrl = imageUrl;

        await _couponRepository.InsertAsync(coupon);
        return ObjectMapper.Map<Coupon, CouponDto>(coupon);
    }

    // Get Coupon by ID
    public async Task<CouponDto> GetAsync(int id)
    {
        var coupon = await _couponRepository.GetAsync(id);
        return ObjectMapper.Map<Coupon, CouponDto>(coupon);
    }

    // Get List of Coupons with Pagination
    public async Task<PagedResultDto<CouponDto>> GetListAsync(GetCouponsInput input)
    {
        var queryable = await _couponRepository.GetQueryableAsync();

        // Apply filter
        queryable = queryable.WhereIf(
            !string.IsNullOrWhiteSpace(input.Filter),
            c => c.Name.Contains(input.Filter) || c.Description.Contains(input.Filter)
        );

        var totalCount = await AsyncExecuter.CountAsync(queryable);

        var items = await AsyncExecuter.ToListAsync(
            queryable
                .OrderBy(input.Sorting ?? nameof(Coupon.Name))
                .PageBy(input.SkipCount, input.MaxResultCount)
        );

        return new PagedResultDto<CouponDto>(
            totalCount,
            ObjectMapper.Map<List<Coupon>, List<CouponDto>>(items)
        );
    }

    // Update Coupon
    public async Task<CouponDto> UpdateAsync(int id, CreateUpdateCouponDto input)
    {
        var coupon = await _couponRepository.GetAsync(id);

        if (input.Image != null)
        {
            coupon.ImageUrl = await _imageService.UploadAsync(input.Image);
        }

        ObjectMapper.Map(input, coupon);
        await _couponRepository.UpdateAsync(coupon);

        return ObjectMapper.Map<Coupon, CouponDto>(coupon);
    }

    // Delete Coupon
    public async Task DeleteAsync(int id)
    {
        //var coupon = await _couponRepository.GetAsync(id);

        //// Optionally delete the image if needed
        //if (!string.IsNullOrEmpty(coupon.ImageUrl))
        //{
        //    await _imageService.de(coupon.ImageUrl);
        //}

        await _couponRepository.DeleteAsync(id);
    }
}
