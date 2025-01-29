namespace Wajba.Models.OrdersDomain;

public class DriveThruOrder
{
    public int Id { get; set; } // Primary key
    public DateTime? Time { get; set; }
    public DateTime? Date { get; set; }


    public string CarColor { get; set; }
    public string CarType { get; set; }
    public string CarNumber { get; set; }
    public int? OrderId { get; set; }

    // Navigation property to Order

    public virtual Order? Order { get; set; }


    public DriveThruOrder()
    {

    }
}