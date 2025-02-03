namespace Wajba.Dtos.CartContract;

public class CartItemDto
{
    public int ItemId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }
    public string ItemName { get; set; } // For quick access in the cart
    public decimal price { get; set; }
    public string Notes { get; set; }
    public int CartItemId { get; set; }
    public string ImgUrl { get; set; }
    public decimal VoucherCode { get; set; }
    public List<ReturnCartItemVariationDto> Variations { get; set; } = new List<ReturnCartItemVariationDto>();
    public List<ReturnCartItemAddonDto> Addons { get; set; } = new List<ReturnCartItemAddonDto>();
    public List<ReturnCartItemExtraDto> Extras { get; set; } = new List<ReturnCartItemExtraDto>();
}
public class ApplyVoucherDto
{
    public decimal Code { get; set; }
}
public class ApplyDiscountDto
{
    public DiscountType discountType { get; set; }
    public decimal amount { get; set; }
}
public class CartDto
{
    public int CustomerId { get; set; }
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    public decimal? TotalAmount { get; set; }
    public decimal? SubTotal { get; set; }
    public decimal? ServiceFee { get; set; }
    public decimal? DeliveryFee { get; set; }
    public decimal? voucherCode { get; set; }
    public decimal? DiscountAmount { get; set; }
    public string Note { get; set; }
}

public class ReturnPosCartDto
{
    public int? userId { get; set; }
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    public decimal? TotalAmount { get; set; }
    public decimal? SubTotal { get; set; }
    public decimal? ServiceFee { get; set; }
    public decimal? DeliveryFee { get; set; }
    public decimal? voucherCode { get; set; }

    public decimal? DiscountAmount { get; set; }
    public string Note { get; set; }
}
public class CartItemVariationDto
{
    public int Id { get; set; }
}

public class CartItemAddonDto
{
    public int Id { get; set; }
}

public class CarItemExtraDto
{
    public int Id { set; get; }
}

public class ReturnCartItemVariationDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal AdditionalPrice { get; set; }
    public string AttributeName { get; set; }  // e.g., Size or other attributes
}

public class ReturnCartItemAddonDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
public class ReturnCartItemExtraDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal AdditionalPrice { get; set; }
}
public class UpdateCartItemDto
{

    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public string ImgUrl { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; } = 1;
    public string Notes { get; set; }
    public decimal price { get; set; }
    //public decimal VoucherCode { get; set; }
    public List<CartItemVariationDto> Variations { get; set; }
    public List<CartItemAddonDto> Addons { get; set; }
    public List<CarItemExtraDto> Extras { get; set; }


}
