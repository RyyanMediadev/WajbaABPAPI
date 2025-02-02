namespace Wajba.Models.OrdersDomain;


public class OrderItemAddon
{
    [Key]
    public int AddonId { get; set; }
    public string AddonName { get; set; }
    public decimal AdditionalPrice { get; set; }
    // Foreign key to OrderItem
    public int OrderItemId { get; set; }
    public virtual OrderItem OrderItem { get; set; }
    public OrderItemAddon()
    {
    }
    //public OrderItemAddon(CartItemAddon a)
    //{
    //    AddonName = a.AddonName;
    //    AdditionalPrice = a.AdditionalPrice;
    //}
}
