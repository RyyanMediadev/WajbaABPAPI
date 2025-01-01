namespace Wajba.Models.OrdersDomain;

public class OrderItem 
{
    public int OrderId { get; set; } // Foreign Key to Order
    public int ItemId { get; set; } // Foreign Key to Item
    public int Quantity { get; set; } = 1; // Default 1
    public decimal Price { get; set; } // Precision (13, 6)
    public decimal TotalPrice { get; set; } = 0; // Default 0, Nullable
    public string Notes { get; set; }

    public ICollection<OrderItemVariation>? SelectedVariations { get; set; }
    public ICollection<OrderItemAddon>? SelectedAddons { get; set; }
    public ICollection<OrderItemExtra>? SelectedExtras { get; set; }

    // Navigation properties
    public virtual Order Order { get; set; }

    public virtual Item Item { get; set; }

    public OrderItem()
    {

    }
}