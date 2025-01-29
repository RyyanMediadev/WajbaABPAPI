
using Wajba.AddSpecification;

namespace Fos_EF.AddSpecification;

public class PopularItemSpecification : BaseSpecification<PopularItem>
{
    public PopularItemSpecification(
    string? name,
    decimal? prePrice,
    decimal? currentPrice,
    string? description,
    int? itemId,
    int? branchId,
    string? categoryName,
    DateTime? createdAtStart,
    DateTime? createdAtEnd,
    bool? isDeleted,
    int? pageNumber,
    int? pageSize)
    {
        if (!string.IsNullOrEmpty(name))
        {
            AddCriteria(p => p.Name == name);
        }
        if (prePrice.HasValue)
        {
            AddCriteria(p => p.PrePrice == prePrice.Value);
        }
        if (currentPrice.HasValue)
        {
            AddCriteria(p => p.CurrentPrice == currentPrice.Value);
        }
        if (!string.IsNullOrEmpty(description))
        {
            AddCriteria(p => p.Description == description);
        }
        if (itemId.HasValue)
        {
            AddCriteria(p => p.ItemId == itemId.Value);
        }
        //if (branchId.HasValue)
        //{
        //    AddCriteria(p => p.BranchId == branchId.Value);
        //}
        //if (!string.IsNullOrEmpty(categoryName))
        //{
        //    AddCriteria(p => p.CategoryName == categoryName);
        //}
        //if (createdAtStart.HasValue)
        //{
        //    AddCriteria(p => p.CreatedAt >= createdAtStart.Value);
        //}
        //if (createdAtEnd.HasValue)
        //{
        //    AddCriteria(p => p.CreatedAt <= createdAtEnd.Value);
        //}
        if (isDeleted.HasValue)
        {
            AddCriteria(p => p.IsDeleted == isDeleted.Value);
        }
        if (pageNumber.HasValue)
            ApplyPaging((pageNumber.Value - 1) * pageSize.Value, pageSize.Value);
    }
}