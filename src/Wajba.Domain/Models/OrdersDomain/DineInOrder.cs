global using Wajba.Models.Orders;

namespace Wajba.Models.OrdersDomain;

public class DineInOrder : FullAuditedEntity<int>
{
    public int Id { get; set; } // Primary key
    public DateTime? Time { get; set; }
    public int NumberOfPersons { get; set; }
    public DateTime? Date { get; set; }
    public int? OrderId { get; set; }

    // Navigation property to Order
    public virtual Order Order { get; set; }

    public DineInOrder()
    {

    }

}