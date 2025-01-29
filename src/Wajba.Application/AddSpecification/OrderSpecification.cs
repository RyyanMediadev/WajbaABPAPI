
using Wajba.AddSpecification;
using Wajba.Models.Orders;


public class OrderSpecification : BaseSpecification<Order>
{
    public OrderSpecification(OrderStatus status)
    {
        AddCriteria(o => o.Status == status);
    }
    public OrderSpecification(OrderStatus status, int numberOfDays, int branchid)
    {
        var startDate = DateTime.UtcNow.Date.AddDays(-numberOfDays);
        var endDate = DateTime.UtcNow.Date;
        AddCriteria(p => p.BranchId == branchid);
        AddCriteria(o => o.Status == status);
        AddCriteria(p => p.CreationTime >= startDate && p.CreationTime <= endDate);

    }
}