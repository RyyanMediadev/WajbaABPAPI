namespace Wajba.Models.CartsDomain;

public class CartItemVariation
{
    //[Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string VariationName { get; set; }
    public string Attributename { get; set; }
    public decimal AdditionalPrice { get; set; }
    // Foreign key to CartItem
    public int CartItemId { get; set; }
    public virtual CartItem? CartItem { get; set; }
    public CartItemVariation( )
    {

    }
}