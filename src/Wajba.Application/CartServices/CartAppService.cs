global using Wajba.Models.Carts;
global using Wajba.Models.Orders;
using Wajba.Dtos.CartContract;
using Wajba.Models.CartsDomain;
using Wajba.Models.ItemVariationDomain;
namespace Wajba.CartService;

[RemoteService(false)]
public class CartAppService : ApplicationService
{
    private readonly IRepository<Cart, int> _CartRepository;
    private readonly IRepository<Item, int> _itemrepo;
    private readonly IRepository<ItemAddon, int> _itemaddonrepo;
    private readonly IRepository<ItemExtra, int> _itemextrarepo;
    private readonly IRepository<ItemVariation, int> _itemvariationrepo;
    private readonly IRepository<CartItem, int> _cartitemrepo;
    public CartAppService(
        IRepository<Cart, int> cartRepository,
        IRepository<Item, int> itemrepo,
        IRepository<ItemAddon,int> itemaddonrepo,
        IRepository<ItemExtra, int> itemextrarepo,
        IRepository<ItemVariation, int> itemvariationrepo,
        IRepository<CartItem, int> cartitemrepo
        )
    {
        _CartRepository = cartRepository;
        _itemrepo = itemrepo;
       _itemaddonrepo = itemaddonrepo;
        _itemextrarepo = itemextrarepo;
        _itemvariationrepo = itemvariationrepo;
        _cartitemrepo = cartitemrepo;
    }
    //CartServices
    //public async Task<Cart> GetCartByCustomeridAsync(int customerId)
    //{
    //    return await _CartRepository.FirstOrDefaultAsync(p => p.CustomerId == customerId);
    //}
    //public async Task<Cart> GetCartByUseridAsync(int UserId)
    //{
    //    // return await _CartRepository.FirstOrDefaultAsync(c => c.userId == UserId);
    //    return await _CartRepository.FirstOrDefaultAsync(/*c => c.userId == UserId*/);
    //}
    //public async Task<Cart> GetCartByCustomerIdAsync(int customerId)
    //{
    //    var cart = await _CartRepository
    //   .FirstOrDefaultAsync(c => c.CustomerId == customerId);
    //    if (cart == null || !cart.CartItems.Any())
    //    {
    //        return null;
    //    }
    //    return cart;
    //}
    //public async Task<Cart> GetCartByEmployeeIdAsync(int userId)
    //{
    //    var cart = await _CartRepository
    //   //.Include(c => c.CartItems)
    //   //      .ThenInclude(i => i.SelectedVariations)
    //   //.Include(c => c.CartItems)
    //   //      .ThenInclude(i => i.SelectedAddons)
    //   //.Include(c => c.CartItems)
    //   //      .ThenInclude(i => i.SelectedExtras)
    //   .FirstOrDefaultAsync(/*c => c.userId == userId*/);
    //    if (cart == null || !cart.CartItems.Any())
    //    {
    //        return null;
    //    }
    //    return cart;
    //}
    //public async Task UpdateCartAsync(Cart cart)
    //{
    //    _CartRepository.UpdateAsync(cart);
    //    // await _CartRepository.sav();
    //}
    public async Task<CartDto> GetCartItemByCustomerAsync(int customerId)
    {
        var cart1 =  _CartRepository.WithDetailsAsync(p => p.CartItems).Result.Include(p => p.CartItems).ThenInclude(p => p.SelectedAddons)
            .Include(p => p.CartItems).ThenInclude(p => p.SelectedExtras).Include(p => p.CartItems).ThenInclude(p => p.SelectedVariations) ;
        Cart cart = await cart1.FirstOrDefaultAsync(p => p.CustomerId == customerId);
        if (cart == null)
            throw new EntityNotFoundException("Cart Not Found");
        return toCartDto(cart);
    }
    private static CartDto toCartDto(Cart cart)
    {
        return new CartDto()
        {
            CustomerId = (int)cart.CustomerId,
            DeliveryFee = cart.DeliveryFee,
            TotalAmount = cart.TotalAmount,
            DiscountAmount = cart.DiscountAmount,
            ServiceFee = cart.ServiceFee,
            Note = cart.Note,
            SubTotal=cart.SubTotal,
            Items = cart.CartItems.Select(p => new CartItemDto()
            {
                Notes = p.Notes,
                ItemId = p.ItemId,
                ItemName=p.ItemName,
                price=p.price,
                Quantity = p.Quantity,
                Addons = p.SelectedAddons.Select(l => new CartItemAddonDto()
                {
                    Id = l.AddonId,
                    Name = l.AddonName,
                    Price = l.AdditionalPrice
                }).ToList(),
                Extras = p.SelectedExtras.Select(m => new ExtraDto()
                {
                    AdditionalPrice = m.AdditionalPrice,
                    Id = m.ExtraId,
                    Name = m.ExtraName,

                }).ToList(),
                Variations = p.SelectedVariations.Select(d => new CartItemVariationDto()
                {
                    Name = d.Attributename,
                    AdditionalPrice = d.AdditionalPrice,
                    AttributeName = d.Attributename,
                    Id = d.Id
                }).ToList()
            }).ToList()
        };
    }
    public async Task<CartDto> CreateAsync(int customerid, List<CartItemDto> cartItemDtos)
    {
        Cart cart = await _CartRepository.FirstOrDefaultAsync(p => p.CustomerId == customerid);
        //if (cart != null)
            //foreach (var i in cart.CartItems)
            //    await _cartitemrepo.HardDeleteAsync(i, true);
        //foreach (var i in await _cartitemrepo.ToListAsync())
        //    if (i.CartId == cart.Id)
        //        await _cartitemrepo.HardDeleteAsync(i, true);
        if (cart == null)
        {
            cart = new Cart()
            {
                CustomerId = customerid,
                TotalAmount = 0,
                SubTotal = 0,
                DeliveryFee = 0,
                DiscountAmount = 0,
                Note = "",
                ServiceFee = 0,
                CartItems = new List<CartItem>(),
            };
            cart = await _CartRepository.InsertAsync(cart, true);
        }
        else
        {
            cart.TotalAmount = 0;
            cart.SubTotal = 0;
            cart.DeliveryFee = 0;
            cart.ServiceFee = 0;
            cart.CartItems = new List<CartItem>();
            cart.Note = "";
            cart.DiscountAmount = 0;
            cart.voucherCode = 0;
            await _CartRepository.UpdateAsync(cart,true);
        };
        bool isfound = true;
        cart.CartItems = new List<CartItem>();
        foreach (var i in cartItemDtos)
        {
            Item item = await _itemrepo.FirstOrDefaultAsync(p => p.Id == i.ItemId);
            if (item == null)
                throw new Exception("Invalid data");
            CartItem cartItem = await _cartitemrepo.FirstOrDefaultAsync(p => p.ItemId == i.ItemId && p.CartId == cart.Id);
            if (cartItem == null)
            {
                isfound = false;
                cartItem = new CartItem()
                {
                    CartId = cart.Id,
                    ItemId = i.ItemId,
                    ItemName = item.Name,
                    price = item.Price,
                    Quantity = i.Quantity,
                    Notes = i.Notes,
                    ImgUrl = item.ImageUrl,
                    Item = item,
                    SelectedAddons = new List<CartItemAddon>(),
                    SelectedVariations = new List<CartItemVariation>(),
                    SelectedExtras = new List<CartItemExtra>()
                };
            }
            else
            {
                cartItem.price = item.Price;
                cartItem.Quantity = i.Quantity;
                cartItem.Notes = i.Notes;
                cartItem.ImgUrl = item.ImageUrl;
                cartItem.SelectedAddons = new List<CartItemAddon>();
                cartItem.SelectedExtras = new List<CartItemExtra>();
                cartItem.SelectedVariations = new List<CartItemVariation>();
            }
            foreach (var j in i.Addons)
            {
                ItemAddon itemAddon = await _itemaddonrepo.FirstOrDefaultAsync(p => p.Id == j.Id && p.ItemId == item.Id);
                if (itemAddon == null)
                    throw new Exception("Invalid data");
                cartItem.SelectedAddons.Add(new CartItemAddon()
                {
                    AdditionalPrice = itemAddon.AdditionalPrice,
                    AddonId = itemAddon.Id,
                    AddonName = itemAddon.AddonName,
                });
                cart.SubTotal += itemAddon.AdditionalPrice;
            }
            foreach (var j in i.Extras)
            {
                ItemExtra itemExtra = await _itemextrarepo.FirstOrDefaultAsync(p => p.Id == j.Id && p.ItemId == item.Id);
                if (itemExtra == null)
                    throw new Exception("Invalid data");
                cartItem.SelectedExtras.Add(new CartItemExtra()
                {
                    AdditionalPrice = itemExtra.AdditionalPrice,
                    ExtraName = itemExtra.Name,
                    ExtraId = itemExtra.Id,

                });
                cart.SubTotal += itemExtra.AdditionalPrice;
            }
            foreach (var j in i.Variations)
            {
                ItemVariation itemVariation = await _itemvariationrepo.WithDetailsAsync(p => p.ItemAttributes).Result.FirstOrDefaultAsync(p => p.Id == j.Id && p.ItemId == item.Id);
                if (itemVariation == null)
                    throw new Exception("Invalid data");
                cartItem.SelectedVariations.Add(new CartItemVariation()
                {
                    VariationName = itemVariation.Name,
                    VariationId = itemVariation.Id,
                    AdditionalPrice = itemVariation.AdditionalPrice,
                    Attributename = itemVariation.ItemAttributes.Name
                });
                cart.SubTotal += itemVariation.AdditionalPrice;
            }

            if (!isfound)
                await _cartitemrepo.InsertAsync(cartItem, true);
            else await _cartitemrepo.UpdateAsync(cartItem, true);
            cart.CartItems.Add(cartItem);
        }
        cart.SubTotal += cart.CartItems.Sum(p => p.Quantity * p.price);
        cart.TotalAmount = cart.SubTotal + cart.ServiceFee + cart.DeliveryFee + cart.DiscountAmount;
        await _CartRepository.UpdateAsync(cart, true);
        return toCartDto(cart);
    }
    //public async Task<IEnumerable<CartItem>> GetCartItemsByCustomerIdAsync(int customerId)
    //{
    //    var carts = await _CartRepository.WithDetailsAsync(p => p.CartItems);

    //    return await _cartitemrepo.GetListAsync();           //.Include(ci => ci.cart)
    //}
    ////public async Task<CartItem> GetCartItemByCustomerAndItemIdAsync(int customerId, int cartItemId)
    ////{
    ////    return await _CartItemRepository
    ////        //.Include(ci => ci.cart)  
    ////        .FirstOrDefaultAsync(ci => ci.cart.userId == customerId && ci.Id == cartItemId */);
    ////}

    ////public async Task<IEnumerable<CartItem>> GetCartItemsByCustomerIdAsync(int customerId)
    ////{
    ////    return await _CartItemRepository.GetListAsync(/*ci => ci.cart.userId == customerId);
    //    //.Include(ci => ci.cart)  
    //    //    .Where(ci => ci.cart.userId == customerId)  
    //    //                   .ToListAsync();
    ////}

    //public async Task RemoveAsync(CartItem cartItem)
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task RemoveAsync(Cart cart)
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task<Order> GetOrderByIdAsync(int id)
    //{
    //    throw new NotImplementedException();
    //}
}