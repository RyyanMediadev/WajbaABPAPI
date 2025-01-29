

using Wajba.AddSpecification;


public class ItemSpecification : BaseSpecification<Item>
{
    public ItemSpecification(
    string? name,
    decimal? price,
    int? categoryId,
    decimal? TaxValue,
     ItemType? ItemType,
    bool? isActive,
    bool? isFeatured,
    int? branchId,
    int? pageNumber,
    int? pageSize)
    {
        if (!string.IsNullOrEmpty(name))
        {
            AddCriteria(i => i.Name == name);
        }

        if (price.HasValue)
        {
            AddCriteria(i => i.Price == price.Value);
        }
        if (categoryId.HasValue)
        {
            AddCriteria(i => i.CategoryId == categoryId.Value);
        }
        if (TaxValue.HasValue)
        {
            AddCriteria(i => i.TaxValue == TaxValue.Value);
        }
        if (ItemType.HasValue)
        {
            AddCriteria(p => p.ItemType == ItemType.Value);
        }
        //if (isActive.HasValue)
        //{
        //    AddCriteria(i => i.status == (isActive.Value ? Status.Active : Status.InActive));
        //}

        if (isFeatured.HasValue)
        {
            AddCriteria(i => i.IsFeatured == isFeatured.Value);
        }

        //if (branchId.HasValue)
        //{
        //    AddCriteria(i => i.b == branchId.Value);
        //}

        // Apply pagination
        if (pageNumber.HasValue)
            ApplyPaging((pageNumber.Value - 1) * pageSize.Value, pageSize.Value);
    }
}