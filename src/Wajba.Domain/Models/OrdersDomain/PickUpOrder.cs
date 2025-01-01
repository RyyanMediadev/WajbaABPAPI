namespace Wajba.Models.OrdersDomain;


public class PickUpOrder
{
    public int Id { get; set; } // Primary key
    public DateTime? Time { get; set; }


    public int? OrderId { get; set; }

    // Navigation property to Order
    public virtual Order? Order { get; set; }

    public PickUpOrder()
    {

    }
}
