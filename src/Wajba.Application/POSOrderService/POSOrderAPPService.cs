global using Wajba.Dtos.OTPContract;
global using Wajba.Models.OTPDomain;
global using Volo.Abp.Domain.Entities;
using Wajba.Models.Orders;
using Wajba.Models.OrdersDomain;
using Wajba.Models.Carts;

namespace Wajba.OTPService;

[RemoteService(false)]
public class POSOrderAPPService : ApplicationService
{
    private readonly IRepository<Order, int> _OrderRepository;
    private readonly IRepository<Cart, int> _CartRepository;


    public POSOrderAPPService(IRepository<Order, int> repository)
    {
        _OrderRepository = repository;
    }
    //public async Task<Cart> GetCartByCustomeridAsync(string customerId)
    //{
    //    return await _CartRepository
    //                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
    //}
    //public async Task<Cart> GetCartByUseridAsync(int UserId)
    //{
    //    return await _CartRepository
    //                        .FirstOrDefaultAsync(c => c.userId == UserId);
    //}
    //public async Task<Cart> GetCartByCustomerIdAsync(string customerId)
    //{
    //    var cart = await _CartRepository
    //   .Include(c => c.CartItems)
    //         .ThenInclude(i => i.SelectedVariations)
    //   .Include(c => c.CartItems)
    //         .ThenInclude(i => i.SelectedAddons)
    //   .Include(c => c.CartItems)
    //         .ThenInclude(i => i.SelectedExtras)
    //   .FirstOrDefaultAsync(c => c.CustomerId == customerId);
    //    if (cart == null || !cart.CartItems.Any())
    //    {
    //        return null;
    //    }
    //    return cart;
    //}
    public async Task<Cart> GetCartByEmployeeIdAsync(int userId)
    {
        var cart = await _CartRepository//.Include(c => c.CartItems)
       //      .ThenInclude(i => i.SelectedVariations)
       //.Include(c => c.CartItems)
       //      .ThenInclude(i => i.SelectedAddons)
       //.Include(c => c.CartItems)
       //      .ThenInclude(i => i.SelectedExtras)
       .FirstOrDefaultAsync(c => c.Id == userId);
        if (cart == null || !cart.CartItems.Any())
        {
            return null;
        }
        return cart;
    }
    public async Task UpdateCartAsync(Cart cart)
    {
        _CartRepository.UpdateAsync(cart);
        //await _context.SaveChangesAsync();
    }
    public async Task AddAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(DineInOrder dineInOrder)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(PickUpOrder pickupOrder)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(DriveThruOrder driveThruOrder)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(PosOrder posOrder)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(PosDeliveryOrder posDeliveryOrder)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(DeliveryOrder deliveryOrder)
    {
        throw new NotImplementedException();
    }

    public double CountPOSOrdersAsync(POSOrderSpecification orderSpec)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetAllPOSOrdersAsync(POSOrderSpecification orderSpec)
    {
        throw new NotImplementedException();
    }

    public async Task<CartItem> GetCartByEmployeeIdAsync(object id)
    {
        throw new NotImplementedException();

        return null;
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<WajbaUser> ValidateTokenAndGetUser(string token)
    {
        throw new NotImplementedException();
        return null;
    }

    public async Task AddOrder(object input)
    {
        throw new NotImplementedException();
    }
}