namespace Wajba.Dtos.CartContract;

public class CreateCartItemDto
{
    public int ItemId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }
    public string Notes { get; set; }
    //public decimal VoucherCode { get; set; }
    public List<CartItemVariationDto> Variations { get; set; } = new List<CartItemVariationDto>();
    public List<CartItemAddonDto> Addons { get; set; } = new List<CartItemAddonDto>();
    public List<CarItemExtraDto> Extras { get; set; } = new List<CarItemExtraDto>();
}
