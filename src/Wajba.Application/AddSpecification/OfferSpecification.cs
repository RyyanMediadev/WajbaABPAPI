

//namespace Fos_EF.AddSpecification;

//public class OfferSpecification : BaseSpecification<Offer>
//{
//    public OfferSpecification(int pageNumber, int pageSize, string name = null, Status? status = null, DateTime? startDate = null, DateTime? endDate = null)
//    {
//        if (!string.IsNullOrEmpty(name))
//        {
//            AddCriteria(o => o.Name == name);
//        }
//        if (status.HasValue)
//        {
//            AddCriteria(o => o.status == status.Value);
//        }
//        if (startDate.HasValue)
//        {
//            AddCriteria(o => o.StartDate >= startDate.Value);
//        }
//        if (endDate.HasValue)
//        {
//            AddCriteria(o => o.EndDate <= endDate.Value);
//        }
//        AddInclude(o => o.OfferItems);
//        AddInclude(o => o.OfferCategories);
//        AddInclude("OfferItems.Item");
//        AddInclude("OfferCategories.Category");

//        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
//    }
//}
