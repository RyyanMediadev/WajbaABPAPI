namespace Wajba.Dtos.CartContract
{


    public class CartItemDto
    {
        public int ItemId { get; set; }
        //public string ItemName { get; set; }
        //public string ImgUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
        public string Notes { get; set; }
        //public decimal price { get; set; }
        //public decimal VoucherCode { get; set; }
        public List<CartItemVariationDto> Variations { get; set; }
        public List<CartItemAddonDto> Addons { get; set; }
        public List<ExtraDto> Extras { get; set; }
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
        public string CustomerId { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public decimal? TotalAmount { get; set; }
        public decimal? ServiceFee { get; set; }
        public decimal? DeliveryFee { get; set; }
        public decimal? voucherCode { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string Note { get; set; }
    }
    public class ReturnCartDto
    {
        public string CustomerId { get; set; }
        public List<ReturnCartItemDto> Items { get; set; } = new List<ReturnCartItemDto>();
        public decimal? TotalAmount { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? ServiceFee { get; set; }
        public decimal? DeliveryFee { get; set; }
        public decimal? voucherCode { get; set; }

        public decimal? DiscountAmount { get; set; }
        public string Note { get; set; }
        public ReturnCartDto()
        {

        }
        //public ReturnCartDto
        //{
        //    CustomerId = existingCart.CustomerId;
        //    voucherCode = existingCart.voucherCode;
        //    ServiceFee = existingCart.ServiceFee;
        //    DeliveryFee = existingCart.DeliveryFee;
        //    Note = existingCart.Note;
        //    TotalAmount = existingCart.TotalAmount;
        //    SubTotal = existingCart.SubTotal;

        //}
    }
    public class ReturnPosCartDto
    {
        public int? userId { get; set; }
        public List<ReturnCartItemDto> Items { get; set; } = new List<ReturnCartItemDto>();
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
        public string Name { get; set; }
        public decimal AdditionalPrice { get; set; }
        public string AttributeName { get; set; }  // e.g., Size or other attributes

    }

    public class CartItemAddonDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    public class ExtraDto
    {

        public string Name { get; set; }
        public decimal AdditionalPrice { get; set; }
    }
    public class ReturnCartItemVariationDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public decimal AdditionalPrice { get; set; }
        public string AttributeName { get; set; }  // e.g., Size or other attributes
        public ReturnCartItemVariationDto()
        {

        }
        //public ReturnCartItemVariationDto(CartItemVariation v)
        //{
        //    id = v.Id;
        //    Name = v.VariationName;
        //    AdditionalPrice = v.AdditionalPrice;
        //    AttributeName = v.Attributename;
        //}
    }

    public class ReturnCartItemAddonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ReturnCartItemAddonDto()
        {

        }
        //public ReturnCartItemAddonDto(CartItemAddon a)
        //{
        //    Id = a.AddonId;
        //    Name = a.AddonName;
        //    Price = a.AdditionalPrice;
        //}
    }
    public class ReturnExtraDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AdditionalPrice { get; set; }
        public ReturnExtraDto()
        {

        }
        //public ReturnExtraDto(CartItemExtra e)
        //{
        //    Id = e.ExtraId;
        //    Name = e.ExtraName;
        //    AdditionalPrice = e.AdditionalPrice;
        //}
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
        public List<ExtraDto> Extras { get; set; }


    }

    public class ReturnCartItemDto
    {
        public int CartItemId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ImgUrl { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; } = 1;
        public string Notes { get; set; }
        public decimal price { get; set; }
        //public decimal VoucherCode { get; set; }
        public List<ReturnCartItemVariationDto> Variations { get; set; }
        public List<ReturnCartItemAddonDto> Addons { get; set; }
        public List<ReturnExtraDto> Extras { get; set; }
        public ReturnCartItemDto()
        {

        }
        //public ReturnCartItemDto(CartItem cart)
        //{
        //    ItemId = cart.ItemId;
        //    ItemName = cart.ItemName;
        //    Quantity = cart.Quantity;
        //    ImgUrl = cart.ImgUrl;
        //    Notes = cart.Notes;
        //    price = cart.price;
        //    CartItemId = cart.Id;
        //}
    }
}