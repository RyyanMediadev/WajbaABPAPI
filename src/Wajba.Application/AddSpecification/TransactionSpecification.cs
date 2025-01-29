

using Wajba.AddSpecification;
using Wajba.Models.OrdersDomain;


public class TransactionSpecification : BaseSpecification<Transactions>
{
    public TransactionSpecification(
        int? id = null,
        int? ordernumber = null,
       string? paymentmethod = null,
       DateTime? dateTime = null,
        int? pageNumber = null,
    int? pageSize = null) : base()
    {
        if (id.HasValue)
            AddCriteria(p => p.Id == id.Value);
        if (ordernumber.HasValue)
            AddCriteria(p => p.OrderId == ordernumber.Value);
        if (!string.IsNullOrEmpty(paymentmethod))
            AddCriteria(p => p.PaymentMethod.ToLower() == paymentmethod.ToLower());
        if (dateTime.HasValue)
            AddCriteria(p => p.CreatedAt == dateTime.Value);
        if (pageNumber.HasValue && pageSize.HasValue)
            ApplyPaging((pageNumber.Value - 1) * pageSize.Value, pageSize.Value);
    }
}