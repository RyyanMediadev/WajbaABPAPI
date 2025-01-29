namespace Wajba.Models.OrdersDomain;

public class DeliveryOrder : FullAuditedEntity<int>
{
    [Required]
    public string Title { get; set; }
    [Required]
    public double Longitude { get; set; }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public DateTime ApproximateTime { get; set; }
    public string? DeliveryBoyId { get; set; }  // Foreign key to DeliveryBoy
    public virtual WajbaUser? DeliveryBoy { get; set; }
    public int? OrderId { get; set; }
    public virtual Order? Order { get; set; }
    public DeliveryOrder( )
    {

    }
}