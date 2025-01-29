

//using Wajba.AddSpecification;

//namespace Fos_EF.AddSpecification;

//public class CouponSpecification : BaseSpecification<Coupon>
//{
//    public CouponSpecification(
//    string? name,
//    string? code,
//    decimal? discount,
//    DiscountType? discountType,
//    DateTime? startDate,
//    DateTime? endDate,
//    decimal? minimumOrderAmount,
//    decimal? maximumDiscount,
//    int? limitPerUser,
//    string? description,
//    int? branchId,
//    bool? isused,
//    bool? isexpire,
//    int pageNumber,
//    int pageSize)
//    {
//        if (!string.IsNullOrEmpty(name))
//        {
//            AddCriteria(c => c.Name.Contains(name));
//        }
//        if (isused.HasValue)
//        {
//            //if (isused.Value)
//            //    AddCriteria(p => p.countofusers > 0);
//            //else AddCriteria(p => p.countofusers == 0);
//        }
//        if (isexpire.HasValue)
//        {
//            if (isexpire.Value)
//                AddCriteria(p => p.EndDate < DateTime.Now || p.countofusers >= p.LimitPerUser);
//            else
//                AddCriteria(p => p.EndDate > DateTime.Now && p.countofusers < p.LimitPerUser);
//        }
//        if (!string.IsNullOrEmpty(code))
//            AddCriteria(c => c.code.ToString() == code);
//        if (discount.HasValue)
//            AddCriteria(c => c.Discount == discount.Value);
//        if (discountType.HasValue)
//        {
//            AddCriteria(c => c.discountType == discountType.Value);
//        }

//        if (startDate.HasValue)
//        {
//            AddCriteria(c => c.StartDate >= startDate.Value);
//        }
//        if (endDate.HasValue)
//        {
//            AddCriteria(c => c.EndDate <= endDate.Value);
//        }
//        if (minimumOrderAmount.HasValue)
//        {
//            AddCriteria(c => c.MinimumOrderAmount >= minimumOrderAmount.Value);
//        }
//        if (maximumDiscount.HasValue)
//        {
//            AddCriteria(c => c.MaximumDiscount <= maximumDiscount.Value);
//        }
//        if (limitPerUser.HasValue)
//        {
//            AddCriteria(c => c.LimitPerUser == limitPerUser.Value);
//        }
//        if (!string.IsNullOrEmpty(description))
//        {
//            AddCriteria(c => c.Description == description);
//        }
//        if (branchId.HasValue)
//        {
//            AddCriteria(c => c.BranchId == branchId.Value);
//        }
//        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
//    }
//}