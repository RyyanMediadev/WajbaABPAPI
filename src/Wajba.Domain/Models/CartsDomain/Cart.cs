global using Wajba.Models.UsersDomain;

namespace Wajba.Models.Carts;

public class Cart: FullAuditedEntity<int>
{
    public Cart()
    {

    }
    [ForeignKey(nameof(Customer))]
    public int? CustomerId { get; set; }
    public virtual WajbaUser Customer { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public decimal? TotalAmount { get; set; }
    public decimal? SubTotal { get; set; }
    public decimal? ServiceFee { get; set; }
    public decimal? DeliveryFee { get; set; }
    public decimal? voucherCode { get; set; }
    public decimal? DiscountAmount { get; set; }
    public string Note { get; set; }
   
}
