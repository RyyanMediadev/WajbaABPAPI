
//using Wajba.AddSpecification;
//using Wajba.Models.Orders;

//namespace Fos_EF.AddSpecification;

//public class OnlineOrderSpecification : BaseSpecification<Order>
//{
//    public OnlineOrderSpecification(
//    int branchId,
//    int? orderId = null,
//    DateTime? dateorder = null,
//    DateTime? startDate = null,
//    DateTime? endDate = null,
//    int? status = null,
//    OrderType? orderType = null,
//    int? fromprice = null,
//    int? toprice = null,
//    int? pageNumber = null,
//    int? pageSize = null) : base()
//    {
//        AddCriteria(o => o.BranchId == branchId && o.CustomerId != null);
//        if (startDate.HasValue)
//        {
//            AddCriteria(o => o.CreatedAt >= startDate.Value);
//        }
//        if (fromprice.HasValue)
//        {
//            AddCriteria(p => p.TotalAmount >= fromprice.Value);
//        }
//        if (toprice.HasValue)
//        {
//            AddCriteria(p => p.TotalAmount <= toprice.Value);
//        }
//        if (orderId.HasValue)
//        {
//            AddCriteria(p => p.Id == orderId.Value);
//        }
//        if (dateorder.HasValue)
//        {
//            AddCriteria(p => p.CreatedAt.Date == dateorder.Value.Date);
//        }
//        if (orderType.HasValue)
//        {
//            AddCriteria(p => p.Ordertype == orderType.Value);
//        }
//        if (endDate.HasValue)
//        {
//            AddCriteria(o => o.CreatedAt <= endDate.Value);
//        }
//        if (status.HasValue)
//        {
//            AddCriteria(o => (int)o.Status == status.Value);
//        }
//        if (pageNumber.HasValue && pageSize.HasValue)
//        {
//            ApplyPaging((pageNumber.Value - 1) * pageSize.Value, pageSize.Value);
//        }
//        // Include related entities
//        AddInclude(o => o.Customer);
//        AddInclude(o => o.Branch);
//        AddInclude(o => o.OrderItems);
//        AddInclude("OrderItems.SelectedVariations");
//        AddInclude("OrderItems.SelectedAddons");
//        AddInclude("OrderItems.SelectedExtras");
//        AddInclude("OrderItems.Item");
//        AddInclude("OrderItems.Item.Category");
//    }
//}