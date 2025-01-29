global using Wajba.Dtos.OTPContract;
global using Wajba.Models.OTPDomain;
global using Volo.Abp.Domain.Entities;
using Wajba.Models.Orders;
using Wajba.Models.OrdersDomain;

namespace Wajba.OTPService;

[RemoteService(false)]
public class POSOrderAPPService : ApplicationService
{
    private readonly IRepository<Order, int> _Orderrepository;

    public POSOrderAPPService(IRepository<Order, int> repository)
    {
        _Orderrepository = repository;
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

    public double CountPOSOrdersAsync(POSOrderSpecification orderSpec)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetAllPOSOrdersAsync(POSOrderSpecification orderSpec)
    {
        throw new NotImplementedException();
    }

    public object GetCartByEmployeeIdAsync(object id)
    {
        throw new NotImplementedException();
    }

    public Order GetOrderByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task ValidateTokenAndGetUser(string token)
    {
        throw new NotImplementedException();
    }
}