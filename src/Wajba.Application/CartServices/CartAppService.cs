

global using Wajba.Models.Carts;
global using Wajba.Dtos.CartContract;
using Wajba.Models.Orders;
namespace Wajba.CartService;
[RemoteService(false)]


public class CartAppService : ApplicationService
{
    private readonly IRepository<Cart, int> _CartRepository;

    private readonly IRepository<CartItem, int> _CartItemRepository;
    //  string customerId = " 6b29fc40-ca47-1067-b31d-00dd010662da";


    public CartAppService(IRepository<CartItem, int> CartItemRepository, IRepository<Cart, int> cartRepository)
    {
        _CartItemRepository = CartItemRepository;
        _CartRepository = cartRepository;
    }




    //CartServices

    public async Task<Cart> GetCartByCustomeridAsync(string customerId)
    {
        return await _CartRepository
                    .FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }
    public async Task<Cart> GetCartByUseridAsync(int UserId)
    {
        // return await _CartRepository.FirstOrDefaultAsync(c => c.userId == UserId);
        return await _CartRepository.FirstOrDefaultAsync(/*c => c.userId == UserId*/);

    }
    public async Task<Cart> GetCartByCustomerIdAsync(string customerId)
    {
        var cart = await _CartRepository
       //.Include(c => c.CartItems)
       //      .ThenInclude(i => i.SelectedVariations)
       //.Include(c => c.CartItems)
       //      .ThenInclude(i => i.SelectedAddons)
       //.Include(c => c.CartItems)
       //      .ThenInclude(i => i.SelectedExtras)
       .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        if (cart == null || !cart.CartItems.Any())
        {
            return null;
        }
        return cart;
    }
    public async Task<Cart> GetCartByEmployeeIdAsync(int userId)
    {
        var cart = await _CartRepository
       //.Include(c => c.CartItems)
       //      .ThenInclude(i => i.SelectedVariations)
       //.Include(c => c.CartItems)
       //      .ThenInclude(i => i.SelectedAddons)
       //.Include(c => c.CartItems)
       //      .ThenInclude(i => i.SelectedExtras)
       .FirstOrDefaultAsync(/*c => c.userId == userId*/);
        if (cart == null || !cart.CartItems.Any())
        {
            return null;
        }
        return cart;
    }
    public async Task UpdateCartAsync(Cart cart)
    {
        _CartRepository.UpdateAsync(cart);
        // await _CartRepository.sav();
    }
    public async Task<CartItem> GetCartItemByCustomerAndItemIdAsync(string customerId, int cartItemId)
    {
        // Retrieve the cart item that belongs to a cart owned by the specified customer
        //return await _CartItemRepository.cartItems
        //    .Include(ci => ci.cart)  // Include Cart to ensure it's filtered by customer
        //    .FirstOrDefaultAsync(ci => ci.cart.CustomerId == customerId && ci.Id == cartItemId);



        CartItem CartItem = await _CartItemRepository
            .FirstOrDefaultAsync(ci => ci.cart.CustomerId == customerId && ci.Id == cartItemId);
        if (CartItem == null)
            throw new Exception("Not Found");
        return CartItem;
        //ObjectMapper.Map<CartItem, CartItemDto>(CartItem);





    }

    public async Task CreateAsync(Cart existingCart)
    {
        throw new NotImplementedException();
    }



    public async Task<IEnumerable<CartItem>> GetCartItemsByCustomerIdAsync(string customerId)
    {
        return await _CartItemRepository.GetListAsync();           //.Include(ci => ci.cart)
                                                                   //.Where(ci => ci.cart.CustomerId == customerId)
                                                                   //                 .ToListAsync();



    }
    public async Task<CartItem> GetCartItemByCustomerAndItemIdAsync(int customerId, int cartItemId)
    {
        return await _CartItemRepository
            //.Include(ci => ci.cart)  
            .FirstOrDefaultAsync(/*ci => ci.cart.userId == customerId && ci.Id == cartItemId*/);
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsByCustomerIdAsync(int customerId)
    {
        return await _CartItemRepository.GetListAsync(/*ci => ci.cart.userId == customerId*/);
        //.Include(ci => ci.cart)  
        //    .Where(ci => ci.cart.userId == customerId)  
        //                   .ToListAsync();
    }

    public async Task RemoveAsync(CartItem cartItem)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Cart cart)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}