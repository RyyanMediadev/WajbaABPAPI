global using Wajba.Dtos.CouponContract;
global using Wajba.Models.CouponsDomain;

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
        string imageUrl = null;
        if (input.Image != null)
        {
            var imagebytes = Convert.FromBase64String(input.Image.Base64Content);
            using var ms = new MemoryStream(imagebytes);
            imageUrl = await _imageService.UploadAsync(ms, input.Image.FileName);
        }
        if (input.StartDate > input.EndDate)
            throw new Exception("Invalid data");
        Coupon coupon = new Coupon()
        {
            Code = input.Code,
            CountOfUsers = input.LimitPerUser,
            LimitPerUser = input.LimitPerUser,
            Description = input.Description,
            Discount = input.Discount,
            DiscountType = (DiscountType)input.DiscountType,
            Name = input.Name,
            EndDate = input.EndDate,
            IsExpired = false,
            StartDate = input.StartDate,
            MaximumDiscount = input.MaximumDiscount,
            MinimumOrderAmount = input.MinimumOrderAmount,
        };
        //var coupon = ObjectMapper.Map<CreateUpdateCouponDto, Coupon>(input);
        coupon.ImageUrl = imageUrl;
        await _couponRepository.InsertAsync(coupon, true);
        return ObjectMapper.Map<Coupon, CouponDto>(coupon);
    }

    // Get Coupon by ID
    public async Task<CouponDto> GetAsync(int id)
    {
        var coupon = await _couponRepository.GetAsync(id);
        if (coupon == null)
            throw new EntityNotFoundException(typeof(Coupon), id);
        return ObjectMapper.Map<Coupon, CouponDto>(coupon);
    }

    // Get List of Coupons with Pagination
    public async Task<PagedResultDto<CouponDto>> GetListAsync(GetCouponsInput input)
    {
        var queryable = await _couponRepository.GetQueryableAsync();

        // Apply filter
        queryable = queryable.WhereIf(
            !string.IsNullOrWhiteSpace(input.name),
            c => c.Name.ToLower() == input.name.ToLower()
        )
        .WhereIf(!string.IsNullOrEmpty(input.code), p => p.Code.ToString().ToLower() == input.code.ToLower())
        .WhereIf(input.discountype.HasValue, p => p.DiscountType == (DiscountType)input.discountype.Value)
        .WhereIf(input.isexpire.HasValue, p => p.IsExpired == input.isexpire.Value);
        var p = input.branchId.HasValue;

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
    public async Task<CouponDto> UpdateAsync(int id, UpdateCoupondto input)
    {
        var coupon = await _couponRepository.GetAsync(id);
        if (coupon == null)
            throw new EntityNotFoundException(typeof(Coupon), id);
        if (input.Image == null)
            throw new Exception("Image is required");
        if (input.Image != null)
        {
            var imagebytes = Convert.FromBase64String(input.Image.Base64Content);
            using var ms = new MemoryStream(imagebytes);
            coupon.ImageUrl = await _imageService.UploadAsync(ms, input.Image.FileName);
        }
        coupon.Description = input.Description;
        coupon.Discount = input.Discount;
        coupon.EndDate = input.EndDate;
        coupon.DiscountType = (DiscountType)input.DiscountType;
        coupon.Name = input.Name;
        coupon.StartDate = input.StartDate;
        coupon.Code = input.Code;
        coupon.CountOfUsers = input.LimitPerUser;
        coupon.MaximumDiscount = input.MaximumDiscount;
        coupon.MinimumOrderAmount = input.MinimumOrderAmount;
        coupon.IsExpired = input.EndDate < DateTime.UtcNow;
        coupon.LimitPerUser = input.LimitPerUser;
        coupon.LastModificationTime = DateTime.UtcNow;
        ObjectMapper.Map(input, coupon);
        await _couponRepository.UpdateAsync(coupon, true);
        return ObjectMapper.Map<Coupon, CouponDto>(coupon);
    }
    public async Task<CouponDto> Updateimage(int id, Base64ImageModel model)
    {
        var coupon = await _couponRepository.GetAsync(id);
        if (coupon == null)
            throw new EntityNotFoundException(typeof(Coupon), id);
        if (model != null)
        {
            var imagebytes = Convert.FromBase64String(model.Base64Content);
            using var ms = new MemoryStream(imagebytes);
            coupon.ImageUrl = await _imageService.UploadAsync(ms, model.FileName);
        }
        coupon.LastModificationTime = DateTime.UtcNow;
        await _couponRepository.UpdateAsync(coupon, true);
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
        if(await _couponRepository.FindAsync(id) == null)
            throw new EntityNotFoundException(typeof(Coupon), id);
        await _couponRepository.DeleteAsync(id);
    }
}
