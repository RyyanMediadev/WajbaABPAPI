namespace Wajba.Models.OrdersDomain;


public class PosDeliveryOrder : FullAuditedEntity<int>
{
    public string BuildingName { get; set; }
    public string ApartmentNumber { get; set; }
    public string Floor { get; set; }
    public string Street { get; set; }
    public string PhoneNumber { get; set; }
    public string AdditionalDirection { get; set; }
    public string AddressLabel { get; set; }
    public int? OrderId { get; set; }
    public virtual Order? Order { get; set; }
    public PosDeliveryOrder( )
    {

    }
    
}