global using Wajba.Models.OrdersDomain;

namespace Wajba.Models.Orders;

public class Order:FullAuditedEntity<int>
{
    public Order()
    {

    }
    public OrderStatus Status { get; set; }
    public OrderType Ordertype { get; set; }
    public PaymentMethod paymentMethod { get; set; }
    public decimal? Discount { get; set; }
    public decimal? TotalAmount { get; set; }
    [ForeignKey(nameof(Branch))]
    public int BranchId { get; set; }
    public virtual Branch Branch { get; set; } = new Branch();
    public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    //foreign key to user table
    //[ForeignKey(nameof(user))]
    public int? userId { get; set; }
    //public virtual User user { get; set; } = new User();
    // Foreign key to Customer
    public string? CustomerId { get; set; }

    // Navigation property to Customer
    public virtual WajbaUser Customer { get; set; } = new WajbaUser();
    public virtual PickUpOrder PickUpOrder { get; set; } = new PickUpOrder();
    public virtual DeliveryOrder DeliveryOrder { get; set; } = new DeliveryOrder();
    public virtual DriveThruOrder DriveThruOrder { get; set; } = new DriveThruOrder();
    public virtual DineInOrder DineInOrder { get; set; } = new DineInOrder();

    public virtual PosOrder PosOrder { get; set; } = new PosOrder();
    public virtual PosDeliveryOrder PosDeliveryOrder { get; set; } = new PosDeliveryOrder();
    [Range(1, 5)]
    public int Review { get; set; }
    public bool IsEditing { get; set; } = false;
}
