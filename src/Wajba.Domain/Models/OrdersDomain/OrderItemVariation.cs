namespace Wajba.Models.OrdersDomain;


public class OrderItemVariation
{

    [Key]
    public int Id { get; set; }
    public string VariationName { get; set; }
    public string Attributename { get; set; }
    public decimal AdditionalPrice { get; set; }

    // Foreign key to OrderItem
    public int OrderItemId { get; set; }
    public virtual OrderItem OrderItem { get; set; }
    public OrderItemVariation()
    {
    }
    //public OrderItemVariation(CartItemVariation v)
    //{
    //    VariationName = v.VariationName;
    //    Attributename = v.Attributename;
    //    AdditionalPrice = v.AdditionalPrice;
    //}
}
