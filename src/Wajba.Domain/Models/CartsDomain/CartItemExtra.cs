﻿namespace Wajba.Models.CartsDomain;
public class CartItemExtra
{
    [Key]
    public int ExtraId { get; set; }
    public string ExtraName { get; set; }
    public decimal AdditionalPrice { get; set; }
    // Foreign key to CartItem
    public int CartItemId { get; set; }
    public virtual CartItem? CartItem { get; set; }

}
