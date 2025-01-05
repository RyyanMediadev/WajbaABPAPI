namespace Wajba.Models.OrderSetup;

public class OrderSetup : FullAuditedEntity<int>
{
    public int FoodPreparationTime { get; set; }
    public int ScheduleOrderSlotDuration { get; set; }
    public int FreeDeliveryKilometer { get; set; }
    public decimal BasicDeliveryCharge { get; set; }
    public decimal ChargePerKilo { get; set; }
    public bool IsTakeawayEnabled { get; set; }
    public bool IsDeliveryEnabled { get; set; }
    public string Ontime { get; set; }
    public string Warning { get; set; }
    public string DelayTime { get; set; }
    public OrderSetup()
    {

    }
}