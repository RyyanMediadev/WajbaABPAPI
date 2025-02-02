global using Wajba.Dtos.OTPContract;
global using Wajba.Models.OTPDomain;
global using Volo.Abp.Domain.Entities;
using Wajba.Models.Orders;
using Wajba.Models.OrdersDomain;
using Wajba.Models.Carts;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Drawing.Printing;
using Wajba.Models.BranchDomain;
using Volo.Abp.Specifications;
using Wajba.AddSpecification;
using Wajba.Dtos.OrderContract;
using Wajba.Settings;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Volo.Abp.Uow;
using System;
using System.Linq;
using Fos_EF.AddSpecification;
using Volo.Abp.Domain.Repositories;

namespace Wajba.OTPService;

[RemoteService(false)]
public class POSOrderAPPService : ApplicationService
{
    private readonly IRepository<Order, int> _OrderRepository;
    private readonly IRepository<Cart, int> _CartRepository;
    private readonly IRepository<Branch, int> _branchRepository;

    private readonly IRepository<DeliveryOrder, int> _DeliveryOrderrepositry;
    private readonly IRepository<DineInOrder, int> _DineInOrderRepositry;
    private readonly IRepository<PickUpOrder, int> _PickUpOrderrepositry;
    private readonly IRepository<DriveThruOrder, int> _DriveThruOrderRepositry;

    private readonly IRepository<PosOrder, int> _PosOrderRepositry;

    private readonly IRepository<PosDeliveryOrder, int> _PosDeliveryOrderRepositry;






    public POSOrderAPPService(
        IRepository<Order, int> orderrepository,
        IRepository<Cart, int> CartRepository,
        IRepository<Branch, int> branchRepository,

                IRepository<DeliveryOrder, int> DeliveryOrderRepositry,


        IRepository<DineInOrder, int> DineInOrderRepositry,
        IRepository<PickUpOrder, int> PickUpOrderrepositry,
         IRepository<DriveThruOrder, int> DriveThruOrder,
         IRepository<PosOrder, int> PosOrderRepositry,
         IRepository<PosDeliveryOrder, int> PosDeliveryOrderRepositry,






        IUnitOfWork unitOfWork)
    {
        _OrderRepository = orderrepository;
        _CartRepository = CartRepository;
        _branchRepository = branchRepository;
        _PosDeliveryOrderRepositry = PosDeliveryOrderRepositry;

        _DineInOrderRepositry = DineInOrderRepositry;
        _PickUpOrderrepositry = PickUpOrderrepositry;
        _DriveThruOrderRepositry = DriveThruOrder;
        _PosOrderRepositry = PosOrderRepositry;
        _PosDeliveryOrderRepositry = PosDeliveryOrderRepositry;


    }
    public async Task<ServiceResponse> GetAllOrdersForEmployeeAsync(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return new ServiceResponse(false, "Token is required.");
        }

        //var employee = await ValidateTokenAndGetUser(token);
        //if (employee == null)
        //{
        //    return new ServiceResponse(false, "Invalid token or employee not found.");
        //}

        string employeeId = "4";
        var orders = await GetOrdersByEmployeeIdAsync(employeeId);

        if (orders == null || !orders.Any())
        {
            return new ServiceResponse(false, "No orders found for this employee.");
        }

        var orderDtos = orders.Select(order => new DashboardOrderDto(order)
        {
            Items = order.OrderItems?.Select(oi => new DashboardOrderItemDto(oi)).ToList()
        }).ToList();

        return new ServiceResponse(true, "Orders retrieved successfully.", orderDtos);
    }


    public async Task<List<Order>> GetOrdersByEmployeeIdAsync(string employeeId)
    {
        return await _OrderRepository
            //.WhereIf(o => o. == employeeId)
            //.Include(o => o.Branch)
            //.Include(o => o.OrderItems)
            //.ThenInclude(oi => oi.Item)
            .ToListAsync();
    }

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

    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ServiceResponse(bool success, string message, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }


    public async Task<ServiceResponse> AddOrderAsync(OrderDTO orderDto, string token)
    {
        if (orderDto == null)
        {
            return new ServiceResponse(false, "Order details are required.");
        }

        if (string.IsNullOrEmpty(token))
        {
            return new ServiceResponse(false, "Token is required.");
        }

        //var employee = await ValidateTokenAndGetUser(token);
        //if (employee == null)
        //{
        //    return new ServiceResponse(false, "Invalid token or employee not found.");
        //}
        int employeeId = 4;
        var existingCart = await GetCartByEmployeeIdAsync(employeeId);
        if (existingCart == null || existingCart.CartItems.Count == 0)
        {
            return new ServiceResponse(false, "Employee cart is empty.");
        }

        var branch = await GetOrderByIdAsync(orderDto.BranchId);
        if (branch == null)
        {
            return new ServiceResponse(false, "Invalid BranchId.");
        }

        var order = new Order()
        {
            Status = (OrderStatus)orderDto.Status,
            Ordertype = (OrderType)orderDto.Ordertype,
            paymentMethod = (PaymentMethod)orderDto.paymentMethod,
            BranchId = orderDto.BranchId,
            userId = /*employee.Id*/ 4,
            Discount = existingCart.DiscountAmount,
            TotalAmount = existingCart.TotalAmount,
            OrderItems = new List<OrderItem>()
        };

        foreach (var cartItem in existingCart.CartItems)
        {
            var orderItem = new OrderItem()
            {
                // Map necessary properties here
            };
            order.OrderItems.Add(orderItem);
        }

        // await _unitOfWork.Orders.AddAsync(order);

        Order Order = await _OrderRepository.InsertAsync(order, true);
        // return ObjectMapper.Map<Site, SiteDto>(site1);

        // await _unitOfWork.CompleteAsync();

        var orderType = (OrderType)orderDto.Ordertype;

        switch (orderType)
        {
            case OrderType.Delivery:
                var deliveryOrder = new DeliveryOrder() { Order = order };

                DeliveryOrder DeliveryOrder = await _DeliveryOrderrepositry.InsertAsync(deliveryOrder, true);


                //   await _unitOfWork.DeliveryOrders.AddAsync(deliveryOrder);
                break;

            case OrderType.DineIn:
                var dineInOrder = new DineInOrder
                {
                    Time = SettingFile.TryParseTime(orderDto.DineInOrder.Time),
                    NumberOfPersons = orderDto.DineInOrder.NumberOfPersons,
                    Date = SettingFile.TryParseDate(orderDto.DineInOrder.Date),
                    Order = order
                };

                DineInOrder DineInOrder = await _DineInOrderRepositry.InsertAsync(dineInOrder, true);

                // await _unitOfWork.DineInOrders.AddAsync(dineInOrder);
                break;

            case OrderType.PickUp:
                var pickupOrder = new PickUpOrder
                {
                    Time = SettingFile.TryParseTime(orderDto.PickUpOrder.Time),
                    Order = order
                };

                PickUpOrder PickUpOrder = await _PickUpOrderrepositry.InsertAsync(pickupOrder, true);

                //   await _unitOfWork.PickUpOrders.AddAsync(pickupOrder);

                break;

            case OrderType.DriveThru:
                var driveThruOrder = new DriveThruOrder
                {
                    Time = SettingFile.TryParseTime(orderDto.DriveThruOrder.Time),
                    Date = SettingFile.TryParseDate(orderDto.DriveThruOrder.Date),
                    CarType = orderDto.DriveThruOrder.CarType,
                    CarNumber = orderDto.DriveThruOrder.CarNumber,
                    CarColor = orderDto.DriveThruOrder.CarColor,
                    Order = order
                };
                DriveThruOrder DriveThruOrder = await _DriveThruOrderRepositry.InsertAsync(driveThruOrder, true);

                ///  await _unitOfWork.DriveThruOrders.AddAsync(driveThruOrder);
                break;

            case OrderType.PosOrder:
                var posOrder = new PosOrder()
                {
                    PhoneNumber = orderDto.posOrder.PhoneNumber,
                    TokenNumber = orderDto.posOrder.TokenNumber
                };


                // await _unitOfWork.posOrders.AddAsync(posOrder);

                PosOrder posOrderinsert = await _PosOrderRepositry.InsertAsync(posOrder, true);


                break;

            case OrderType.PosDelivery:
                var posDeliveryOrder = new PosDeliveryOrder()
                {
                    // Add relevant fields
                };
                PosDeliveryOrder posDeliveryOrders = await _PosDeliveryOrderRepositry.InsertAsync(posDeliveryOrder, true);

                // await _unitOfWork.posDeliveryOrders.AddAsync(posDeliveryOrder);
                break;

            default:
                return new ServiceResponse(false, "Invalid order type.");
        }

        //await _unitOfWork.CompleteAsync();

        //// Clear the cart after order creation
        //existingCart.CartItems.Clear();
        //await _unitOfWork.carts.RemoveAsync(existingCart);
        //await _unitOfWork.CompleteAsync();

        return new ServiceResponse(true, "Order created successfully.");
    }







    //private async Task<WajbaUser> ValidateTokenAndGetUser(string token)
    //{
    //    var principal = GetPrincipalFromToken(token);
    //    if (principal == null)
    //    {
    //        return null;
    //    }
    //    var userIdClaim = principal.FindFirst("Id");
    //    if (userIdClaim == null)
    //    {
    //        return null;
    //    }
    //    var userId = userIdClaim.Value;
    //    return await _unitOfWork.ussers.GetByIdAsync(int.Parse(userId));
    //}

    //private ClaimsPrincipal GetPrincipalFromToken(string token)
    //{
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
    //    try
    //    {
    //        return tokenHandler.ValidateToken(token, new TokenValidationParameters
    //        {
    //            ValidateIssuer = true,
    //            ValidateAudience = true,
    //            ValidateLifetime = true,
    //            ValidateIssuerSigningKey = true,
    //            ValidIssuer = _configuration["Jwt:Issuer"],
    //            ValidAudience = _configuration["Jwt:Audience"],
    //            IssuerSigningKey = new SymmetricSecurityKey(key)
    //        }, out SecurityToken validatedToken);
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //}
    public async Task<ServiceResponse> DeleteOrderAsync(int orderId, string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return new ServiceResponse(false, "Token is required.");
        }

        // Validate token and get employee
        //var employee = await ValidateTokenAndGetUser(token);
        //if (employee == null)
        //{
        //    return new ServiceResponse(false, "Invalid token or employee not found.");
        //}

        // Fetch the order
        //    var order = await _unitOfWork.Orders.GetByIdAsync(orderId);

        var order = _OrderRepository.GetAsync(orderId);
        if (order == null)
        {
            return new ServiceResponse(false, "Order not found.");
        }

        // Check if the order belongs to the employee
        //if (order.userId != employee.Id)
        //{
        //    return new ServiceResponse(false, "You do not have permission to delete this order.");
        //}

        // Remove the order
        await _OrderRepository.DeleteAsync(orderId);
        //await _OrderRepository.CompleteAsync();

        return new ServiceResponse(true, "Order deleted successfully.");
    }


    public async Task<ServiceResponse> GetOrderByIdAsync(int id)
    {
        try
        {
            // var order = await _unitOfWork.Orders.GetByIdAsync(id);

            var order = await _OrderRepository.GetAsync(id);

            if (order == null)
            {
                return new ServiceResponse(false, "Order not found.");
            }

            var orderDto = new DashboardOrderDto(order)
            {
                Items = order.OrderItems?.Select(oi => new DashboardOrderItemDto(oi)).ToList()
            };

            return new ServiceResponse(true, "Order retrieved successfully.", orderDto);
        }
        catch (Exception ex)
        {
            // Log the exception
            return new ServiceResponse(false, $"An error occurred: {ex.Message}");
        }
    }




    public async Task<double> CountPOSOrdersAsync(AddSpecification.ISpecification<Order> spec)
    {
        IQueryable<Order> query = (IQueryable<Order>)_OrderRepository.GetCountAsync();
        if (spec.Criteria != null)
            foreach (var criteria in spec.Criteria)
                query = query.Where(criteria);
        return await query.CountAsync();


    }


    public async Task<List<DashboardOrderDto>> GetAllPOSOrdersAsync(POSOrderSpecification spec)
    {
        IQueryable<Order> query = await _OrderRepository.GetQueryableAsync();

        if (spec.Criteria != null)
        {
            foreach (var criteria in spec.Criteria)
            {
                query = query.Where(criteria);
            }
        }

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        var orderDtos = await query
            .Select(order => new DashboardOrderDto(order)
            {
                Items = order.OrderItems.Select(oi => new DashboardOrderItemDto(oi)).ToList()
            })
            .ToListAsync();

        return orderDtos;
    }


    public async Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId)
    {
        if (customerId ==0)
        {
            return new List<Order>(); // Return empty list if customerId is invalid
        }
         var items = await _OrderRepository.WithDetailsAsync(
           x => x.Branch,
           x => x.DineInOrder,
           x => x.PickUpOrder,
           x => x.PosDeliveryOrder,
           x => x.PosOrder,
           x => x.OrderItems,
               x => x.PosOrder);
        return (List<Order>)items;
        //return await _context.Orders
        //    .Where(o => o.CustomerId == customerId)
        //    .Include(o => o.Branch)
        //    .Include(o => o.DineInOrder)
        //    .Include(o => o.PickUpOrder)
        //    .Include(o => o.PosDeliveryOrder)
        //    .Include(o => o.PosOrder)
        //    .Include(o => o.OrderItems)
        //        .ThenInclude(oi => oi.Item)
        //    .Include(o => o.OrderItems)
        //        .ThenInclude(oi => oi.SelectedVariations)
        //    .Include(o => o.OrderItems)
        //        .ThenInclude(oi => oi.SelectedExtras)
        //    .Include(o => o.OrderItems)
        //        .ThenInclude(oi => oi.SelectedAddons)
        //    .ToListAsync() ?? new List<Order>();
    }

    //public async Task<OrderSetupDto> GetAllOrdersForCustomer(int branchid, int? pagesize = null, int? pagenumber = null)
    //{



    //    Branch branch = await _branchRepository.GetAsync(branchid);
    //    if (branch == null)

    //        throw new Exception("Branch Not Found");


    //    var orders = await GetOrdersByCustomerIdAsync(customer.Id, branchid);
    //    if (orders == null || !orders.Any())
    //    {
    //        return Ok(new { success = false, message = "No orders found for this customer." });
    //    }
    //    int totalPages = 1;
    //    if (pagenumber.HasValue)
    //    {
    //        pagesize = 10;
    //        var p = orders.Skip((pagenumber.Value - 1) * pagesize.Value).Take(pagesize.Value);
    //        totalPages = (int)Math.Ceiling((double)orders.Count / 10.0);
    //        var orderDtos = p.Select(order => new DashboardOrderDto(order)
    //        {
    //            Items = order.OrderItems?.Select(oi => new DashboardOrderItemDto(oi)
    //            {
    //            }).ToList()
    //        }).ToList();

    //        return Ok(new
    //        {
    //            success = true,
    //            Data = orderDtos,
    //            TotalCount = orders.Count,
    //            PageNumber = pagenumber,
    //            TotalPages = totalPages
    //        });
    //    }
    //    var orderDtoss = orders.Select(order => new DashboardOrderDto(order)
    //    {
    //        Items = order.OrderItems?.Select(oi => new DashboardOrderItemDto(oi)
    //        {
    //        }).ToList()
    //    }).ToList();


    //    var insertedOrderSetup = await _orderSetupRepository.InsertAsync(orderSetup, true);
    //    return ObjectMapper.Map<OrderSetup, OrderSetupDto>(insertedOrderSetup);
    //}


    public async Task<ServiceResponse> GetAllOrdersForCustomerAsync(int customerId, int branchId, int? pageSize = null, int? pageNumber = null)
    {
        try
        {
            var branch = await _branchRepository.GetAsync(branchId);
            if (branch == null)
            {
                return new ServiceResponse(false, "Branch not found.");
            }

            var orders = await GetOrdersByCustomerIdAsync(customerId);
            if (orders == null || !orders.Any())
            {
                return new ServiceResponse(false, "No orders found for this customer.");
            }

            int totalCount = orders.Count();
            int totalPages = 1;

            if (pageSize.HasValue && pageNumber.HasValue && pageSize.Value > 0)
            {
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize.Value);
                orders = orders.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
            }

            var orderDtos = orders.Select(order => new DashboardOrderDto(order)
            {
                Items = order.OrderItems?.Select(oi => new DashboardOrderItemDto(oi)).ToList()
            }).ToList();

            return new ServiceResponse(true, "Orders retrieved successfully.", new
            {
                Data = orderDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            });
        }
        catch (Exception ex)
        {
            return new ServiceResponse(false, $"An error occurred: {ex.Message}");
        }
    }




    //    Orders

    public async Task<ServiceResponse> UpdateOrderStatusAsync(int orderId, string status/* IHubContext<OrderHub> hubContext*/)
    {
        try
        {


            //Category category = await _categoryRepository.GetAsync(id);
            //if (category == null)
                //throw new Exception("Not found");
            //var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            Order order = await _OrderRepository.GetAsync(orderId);

            if (order == null)
            {
                return new ServiceResponse(false, "Order not found.");
            }

            if (!Enum.TryParse<OrderStatus>(status, out var newStatus))
            {
                return new ServiceResponse(false, "Invalid order status.");
            }

            order.Status = newStatus;

            //    await _unitOfWork.Orders.UpdateAsync(order);

            Order site1 = await _OrderRepository.UpdateAsync(order, true);



            // await _unitOfWork.CompleteAsync();

            // Notify all dashboard clients
            //await hubContext.Clients.All.SendAsync("OrderUpdated", orderId, status);

            //// Notify the specific customer
            //await hubContext.Clients.User(order.CreatorId.ToString())
            //    .SendAsync("OrderStatusChanged", orderId.ToString(), status);

            return new ServiceResponse(true, "Order status updated successfully.");
        }
        catch (Exception ex)
        {
            return new ServiceResponse(false, $"An error occurred: {ex.Message}");
        }
    }

    public async Task<ServiceResponse> DeleteOrderAsync2(int orderId, string token)
    {
        try
        {
            if (string.IsNullOrEmpty(token))
            {
                return new ServiceResponse(false, "Token is required.");
            }

            // Validate token and get customer
            //var customer = await ValidateTokenAndGetUser(token);
            //if (customer == null)
            //{
            //    return new ServiceResponse(false, "Invalid token or customer not found.");
            //}

            // Fetch the order
            //   var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            var order = _PosOrderRepositry.GetAsync(orderId);
            if (order == null)
            {
                return new ServiceResponse(false, "Order not found.");
            }

            // Check if the order belongs to the customer
            //if (order.CustomerId != customer.Id)
            //{
            //    return new ServiceResponse(false, "You do not have permission to delete this order.");
            //}

            // Delete the order

            // await _unitOfWork.Orders.RemoveAsync(order);

            await _OrderRepository.DeleteAsync(orderId);


            //await _unitOfWork.CompleteAsync();

            return new ServiceResponse(true, "Order deleted successfully.");
        }
        catch (Exception ex)
        {
            return new ServiceResponse(false, $"An error occurred: {ex.Message}");
        }
    }

    //public async Task<ServiceResponse> GetDailySalesAsync(int numberOfDays, int branchId)
    //{
    //    try
    //    {
    //        if (numberOfDays <= 0)
    //        {
    //            return new ServiceResponse(false, "The number of days should be greater than zero.");
    //        }

    //        var startDate = DateTime.UtcNow.Date.AddDays(-numberOfDays);
    //        var endDate = DateTime.UtcNow.Date;

    //         Generate all dates in the range
    //        var allDates = Enumerable.Range(0, numberOfDays + 1)
    //            .Select(offset => startDate.AddDays(offset))
    //            .ToList();

    //         Query sales data from the database
    //        var salesData = await _OrderRepository.GetListAsync()
    //            .GetOrders()
    //            .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate && o.BranchId == branchId)
    //            .GroupBy(o => o.CreatedAt.Date)
    //            .Select(g => new
    //            {
    //                Date = g.Key,
    //                TotalSales = g.Sum(o => o.TotalAmount ?? 0)
    //            })
    //            .ToListAsync();

    //         Merge sales data with all dates
    //        var dailySales = allDates
    //            .GroupJoin(
    //                salesData,
    //                date => date,
    //                sales => sales.Date,
    //                (date, sales) => new
    //                {
    //                    Date = date,
    //                    TotalSales = sales.FirstOrDefault()?.TotalSales ?? 0
    //                })
    //            .OrderBy(x => x.Date)
    //            .Select(x => (x.Date, x.TotalSales));

    //        if (!dailySales.Any())
    //        {
    //            return new ServiceResponse(false, "No sales data found for the specified period.");
    //        }

    //        var totalSalesSum = dailySales.Sum(x => x.TotalSales);
    //        var averageSales = dailySales.Average(x => x.TotalSales);

    //        return new ServiceResponse(true, "Daily sales retrieved successfully.", new
    //        {
    //            Data = dailySales,
    //            TotalSalesSum = totalSalesSum,
    //            TotalAverageSales = averageSales
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        return new ServiceResponse(false, $"An error occurred: {ex.Message}");
    //    }
    //}


    //public async Task<ServiceResponse> GetSalesReportAsync(
    //int branchId,
    //DateTime? startDate = null,
    //DateTime? endDate = null,
    //DateTime? dateOrder = null,
    //int? status = null,
    //OrderType? orderType = null,
    //int? orderId = null,
    //int? fromPrice = null,
    //int? toPrice = null,
    //string? paidStatus = null,
    //int? pageNumber = null,
    //int? pageSize = null)
    //{
    //    try
    //    {
    //        var orderSpec = new OnlineOrderSpecification(branchId, orderId, dateOrder, startDate, endDate, status, orderType, fromPrice, toPrice);
    //        var orders = await _unitOfWork.Orders.GetAllOnlineOrdersAsync(orderSpec);
    //        var totalOnlineOrders = await _unitOfWork.Orders.CountOnlineOrdersAsync(orderSpec);

    //        var posOrderSpec = new POSOrderSpecification(branchId, orderId, dateOrder, orderType, startDate, endDate, status, fromPrice, toPrice);
    //        var posOrders = await _unitOfWork.PosOrders.GetAllPOSOrdersAsync(posOrderSpec);
    //        var totalPosOrders = await _unitOfWork.PosOrders.CountPOSOrdersAsync(posOrderSpec);

    //        int totalOrderCount = totalOnlineOrders + totalPosOrders;

    //         Convert to DTOs
    //        var orderDtos = orders.Select(order => new DashboardOrderDto(order)
    //        {
    //            paymentstatus = (PaymentStatus)1,
    //            Items = order.OrderItems?.Select(oi => new DashboardOrderItemDto(oi)).ToList()
    //        }).ToList();

    //        orderDtos.AddRange(posOrders.Select(p => new DashboardOrderDto(p)
    //        {
    //            Items = p.OrderItems.Select(l => new DashboardOrderItemDto(l)).ToList()
    //        }).ToList());

    //         Apply pagination if needed
    //        if (pageNumber.HasValue && pageSize.HasValue && pageNumber.Value > 0 && pageSize.Value > 0)
    //        {
    //            int totalPages = (int)Math.Ceiling((double)totalOrderCount / pageSize.Value);
    //            var pagedOrders = orderDtos.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

    //            return new ServiceResponse(true, "Sales report retrieved successfully.", new
    //            {
    //                Data = pagedOrders,
    //                CountOfOnlineOrders = totalOnlineOrders,
    //                CountOfPOSOrders = totalPosOrders,
    //                TotalCount = totalOrderCount,
    //                PageNumber = pageNumber,
    //                PageSize = pagedOrders.Count(),
    //                TotalPages = totalPages
    //            });
    //        }

    //        return new ServiceResponse(true, "Sales report retrieved successfully.", new
    //        {
    //            Data = orderDtos,
    //            CountOfOnlineOrders = totalOnlineOrders,
    //            CountOfPOSOrders = totalPosOrders,
    //            TotalCount = totalOrderCount,
    //            PageNumber = 1,
    //            PageSize = totalOrderCount,
    //            TotalPages = 1
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        return new ServiceResponse(false, $"An error occurred: {ex.Message}");
    //    }
    //}


}