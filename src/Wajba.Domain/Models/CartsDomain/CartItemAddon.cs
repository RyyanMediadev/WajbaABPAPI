global using Wajba.Models.Carts;

namespace Wajba.Models.CartsDomain;
public class CartItemAddon
{
    public CartItemAddon()
    {

    }

    //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AddonId { get; set; }
    public string AddonName { get; set; }
    public decimal AdditionalPrice { get; set; }
    // Foreign key to CartItem
    public int CartItemId { get; set; }
    public virtual CartItem? CartItem { get; set; }
}