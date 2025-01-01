global using System.ComponentModel.DataAnnotations.Schema;
global using System.ComponentModel.DataAnnotations;
global using Wajba.Models.CartsDomain;

namespace Wajba.Models.Carts;

public class CartItem
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Item))]
    public int ItemId { get; set; }
    public Item Item { get; set; }
    // Foreign key to CartItem
    public string ImgUrl { get; set; }
    public int CartId { get; set; }
    public virtual Cart cart { get; set; }
    public string ItemName { get; set; } // For quick access in the cart
    public int Quantity { get; set; }  // Quantity of this item in the cart 
    public string Notes { get; set; }
    public decimal price { get; set; }
    public ICollection<CartItemVariation> SelectedVariations { get; set; } = new List<CartItemVariation>();// Variations chosen by the user
    public ICollection<CartItemAddon> SelectedAddons { get; set; } = new List<CartItemAddon>();// Addons chosen by the user
    public ICollection<CartItemExtra> SelectedExtras { get; set; } = new List<CartItemExtra>();
    public CartItem()
    {

    }
}
