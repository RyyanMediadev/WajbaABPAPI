namespace Wajba.Models.OrdersDomain;

public class OrderItemExtra
{
    [Key]
    public int ExtraId { get; set; }
    public string ExtraName { get; set; }
    public decimal AdditionalPrice { get; set; }
    // Foreign key to OrderItem
    public int OrderItemId { get; set; }
    public virtual OrderItem OrderItem { get; set; }
    public OrderItemExtra()
    {

    }
    //public OrderItemExtra(CartItemExtra e)
    //{
    //    ExtraName = e.ExtraName;
    //    AdditionalPrice = e.AdditionalPrice;
    //}
}
