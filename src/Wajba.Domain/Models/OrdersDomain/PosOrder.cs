namespace Wajba.Models.OrdersDomain;
public class PosOrder : FullAuditedEntity<int>
{

    public string PhoneNumber { get; set; }
    public string TokenNumber { get; set; }

    public int? OrderId { get; set; }

    // Navigation property to Order
    public virtual Order? Order { get; set; }

    public PosOrder()
    {

    }
}
