using System.Linq;
using Wajba.Models.Orders;

namespace Wajba.Models.OrdersDomain;

//namespace Wajba.Dtos.DashBoard_OrderContract;


public class DashboardOrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public decimal? TotalAmount { get; set; }
    public OrderType OrderType { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus paymentstatus { get; set; }
    public decimal? Discount { get; set; }
    public string employeeName { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public bool isreviwed { get; set; } = false;
    public int rating {  get; set; }
    public int countofitems {  get; set; }
    public List<DashboardOrderItemDto> Items { get; set; } = new List<DashboardOrderItemDto>();
    public DashboardOrderDto()
    {

    }
    public DashboardOrderDto(Order order)
    {
        Id = order.Id;
        OrderDate = order.CreationTime;
        Status = order.Status;
        TotalAmount = order.TotalAmount;
        OrderType = order.Ordertype;
        PaymentMethod = order.paymentMethod;
        Discount = order.Discount;
        employeeName = order.Customer?.FullName;
        BranchId = order.BranchId;
        BranchName = order.Branch?.Name;
        this.isreviwed = order.IsEditing;
        this.rating = order.Review;
        //this.countofitems = order.OrderItems.Count();
    }
}

public class DashboardOrderItemDto
{
    public int ItemId { get; set; }
    public string imgurl { get; set; }
    public string categoryname { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public string ItemName { get; set; }
    public List<DashboardOrderItemVariationDto> Variations { get; set; } = new List<DashboardOrderItemVariationDto>();
    public List<DashboardOrderItemAddonDto> Addons { get; set; } = new List<DashboardOrderItemAddonDto>();
    public List<DashboardOrderItemExtraDto> Extras { get; set; } = new List<DashboardOrderItemExtraDto>();
    public DashboardOrderItemDto()
    {

    }
    public DashboardOrderItemDto(OrderItem oi)
    {
        ItemId = oi.ItemId;
        Quantity = oi.Quantity;
        Price = oi.Price;
        TotalPrice = oi.TotalPrice;
        ItemName = oi.Item.Name;
        imgurl = oi.Item.ImageUrl;
        //categoryname = oi.Item.Category.name;
        this.Variations = oi.SelectedVariations.Select(p => new DashboardOrderItemVariationDto(p)
        {

        }).ToList();
        this.Addons = oi.SelectedAddons.Select(p => new DashboardOrderItemAddonDto(p)
        {

        }).ToList();
        this.Extras = oi.SelectedExtras.Select(oi => new DashboardOrderItemExtraDto(oi)).ToList();
    }
}

public class DashboardOrderItemVariationDto
{
    public string VariationName { get; set; }
    public string AttributeName { get; set; }
    public decimal AdditionalPrice { get; set; }
    public DashboardOrderItemVariationDto()
    {

    }
    public DashboardOrderItemVariationDto(OrderItemVariation v)
    {
        VariationName = v.VariationName;
        AdditionalPrice = v.AdditionalPrice;
        AttributeName = v.Attributename;
    }

}

public class DashboardOrderItemAddonDto
{
    public string AddonName { get; set; }
    public decimal AdditionalPrice { get; set; }
    public DashboardOrderItemAddonDto()
    {

    }
    public DashboardOrderItemAddonDto(OrderItemAddon a)
    {
        AdditionalPrice = a.AdditionalPrice;
        AddonName = a.AddonName;
    }
}

public class DashboardOrderItemExtraDto
{
    public string ExtraName { get; set; }
    public decimal AdditionalPrice { get; set; }
    public DashboardOrderItemExtraDto()
    {

    }
    public DashboardOrderItemExtraDto(OrderItemExtra e)
    {
        ExtraName = e.ExtraName;
        AdditionalPrice += e.AdditionalPrice;
    }
}
