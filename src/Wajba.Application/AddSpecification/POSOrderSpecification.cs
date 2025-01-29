
using Wajba.AddSpecification;
using Wajba.Models.Orders;


public class POSOrderSpecification : BaseSpecification<Order>
{
    public POSOrderSpecification(
    int branchId,
    int? orderId = null,
    DateTime? orderDate = null,
    OrderType? orderType = null,
    DateTime? startDate = null,
    DateTime? endDate = null,
    int? status = null,
    int? fromprice = null,
    int? toprice = null,
    int? pageNumber = null,
    int? pageSize = null) : base()
    {
        AddCriteria(o => o.BranchId == branchId && o.userId != null);
        if (startDate.HasValue)
        {
            AddCriteria(o => o.CreationTime >= startDate.Value);
        }
        if (orderType.HasValue)
        {
            AddCriteria(p => p.Ordertype == orderType);
        }
        if (orderId.HasValue)
        {
            AddCriteria(p => p.Id == orderId);
        }
        if (fromprice.HasValue)
            AddCriteria(p => p.TotalAmount >= fromprice.Value);
        if (toprice.HasValue)
            AddCriteria(p => p.TotalAmount <= toprice.Value);
        if (orderDate.HasValue)
        {
            AddCriteria(p => p.CreationTime.Date == orderDate.Value.Date);
        }
        if (endDate.HasValue)
        {
            AddCriteria(o => o.CreationTime <= endDate.Value);
        }
        if (status.HasValue)
        {
            AddCriteria(o => (int)o.Status == status.Value);
        }
        if (pageNumber.HasValue && pageSize.HasValue)
        {
            ApplyPaging((pageNumber.Value - 1) * pageSize.Value, pageSize.Value);
        }

        //AddInclude(o => o.wa);
        AddInclude(o => o.Branch);
        AddInclude(o => o.OrderItems);
        AddInclude("OrderItems.SelectedVariations");
        AddInclude("OrderItems.SelectedAddons");
        AddInclude("OrderItems.SelectedExtras");
        AddInclude("OrderItems.Item");
    }
}