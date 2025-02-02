﻿namespace Wajba.Models.CartsDomain;
public class CartItemExtra
{
    public CartItemExtra()
    {

    }
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ExtraId { get; set; }
    public string ExtraName { get; set; }
    public decimal AdditionalPrice { get; set; }
    // Foreign key to CartItem
    public int CartItemId { get; set; }
    public virtual CartItem? CartItem { get; set; }

}
