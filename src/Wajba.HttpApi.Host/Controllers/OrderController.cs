
using Fos_EF.AddSpecification;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Wajba.Categories;
using Wajba.Controllers;
using Wajba.Dtos.OrderContract;
using Wajba.Enums;
using Wajba.Hubs;
using Wajba.Models.BranchDomain;
using Wajba.Models.Orders;
using Wajba.Models.OrdersDomain;
using Wajba.OTPService;
using Wajba.Settings;

namespace FosAPI.Controllers;

//[Authorize]
public class OrderController : WajbaController
{
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly POSOrderAPPService _POSOrderAPPService;
    //private readonly ITokenService _tokenService;

    public OrderController(
        //IUnitOfWork unitOfWork,
        //UserManager<ApplicationUser> userManager,
        IConfiguration configuration, POSOrderAPPService POSOrderAPPService
        //ITokenService tokenService
        )
    {
        //_unitOfWork = unitOfWork;
        //_userManager = userManager;
        _POSOrderAPPService = POSOrderAPPService;
        _configuration = configuration;
        //_tokenService = tokenService;
    }
    //[HttpGet("customer-orders")]
    ////[Authorize]
    //public async Task<IActionResult> GetAllOrdersForCustomer(int branchid, int? pagesize = null, int? pagenumber = null)
    //{
    //    var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    if (string.IsNullOrEmpty(token))
    //    {
    //        return Ok(new { success = false, message = "Token is required" });
    //    }
    //    var customer = await ValidateTokenAndGetUser(token);
    //    if (customer == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    }

    //    await _pOSOrderAPPService.GetAllOrdersForCustomer(branchid, pagesize = null, pagenumber = null);

    //    return Ok(new
    //    {
    //        success = true,
    //        Data = orderDtoss,
    //        TotalCount = orders.Count(),
    //        PageNumber = 1,
    //        TotalPages = totalPages
    //    });
    //}

    [HttpGet("Kitchen-customer-orders")]
    public async Task<IActionResult> GetAllOrdersForCustomer(int branchId, int? pageSize = null, int? pageNumber = null)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (string.IsNullOrEmpty(token))
        {
            return Ok(new { success = false, message = "Token is required" });
        }

        //var customer = await ValidateTokenAndGetUser(token);
        //if (customer == null)
        //{
        //    return Ok(new { success = false, message = "Invalid token or customer not found" });
        //}
        int customerId = 4;
        var response = await _POSOrderAPPService.GetAllOrdersForCustomerAsync(customerId, branchId, pageSize, pageNumber);

        return Ok(new
        {
            success = response.Success,
            message = response.Message,
            data = response.Data
        });
    }


    //[HttpGet("top-customers")]
    //public async Task<IActionResult> GetTopCustomersByOrderCount(int branchid, [FromQuery] int topN = 10, [FromQuery] int pagenumber = 1)
    //{
    //    var topCustomers = await _unitOfWork.orderRepo.GetTopCustomersByOrderCountAsync(topN, pagenumber, branchid);
    //    if (!topCustomers.Any())
    //    {
    //        return Ok(new { success = false, message = "No customers found." });
    //    }
    //    var result = topCustomers.Select(c => new
    //    {
    //        c.Customer.Id,
    //        c.Customer.fullName,
    //        c.Customer.Email,
    //        c.Customer.PhoneNumber,
    //        OrderCount = c.OrderCount
    //    });
    //    int totalCount = await _unitOfWork.orderRepo.GetcountCustomersByOrderAsync(branchid);
    //    int totalPages = 1;
    //    totalPages = (int)Math.Ceiling((double)totalCount / topN);
    //    return Ok(new
    //    {
    //        success = true,
    //        data = result,
    //        TotalCount = totalCount,
    //        PageNumber = pagenumber,
    //        TotalPages = totalPages
    //    });
    //}


    [HttpGet("daily-sales")]
    public async Task<IActionResult> GetDailySales(int branchid, [FromQuery] int numberOfDays)
    {
        if (numberOfDays <= 0)
        {
            return Ok(new { success = false, message = "The number of days should be greater than zero." });
        }
        //var dailySales = await _unitOfWork.orderRepo.GetDailySalesAsync(numberOfDays, branchid);
        var dailySales = await _POSOrderAPPService.GetDailySalesAsync(numberOfDays, branchid);

        if (!dailySales.Any())
        {
            return Ok(new { success = false, message = "No sales data found for the specified period." });
        }
        //var totalSalesSum = dailySales.Sum(x => x.TotalSales);
        //var averageSales = dailySales.Average(x => x.TotalSales);

        //var response =
        //     dailySales.Select(d => new
        //     {
        //         d.Date,
        //         TotalSales = d.TotalSales
        //     });
        return Ok(new
        {
            success = true,
            //data = response,
            //TotalSalesSum = totalSalesSum,
            //TotalAverageSales = averageSales
        });
    }

 

    //[HttpGet("daily-sales")]
    //public async Task<IActionResult> GetDailySales(int branchId, [FromQuery] int numberOfDays)
    //{
    //    var response = await _POSOrderAPPService.GetDailySalesAsync(numberOfDays, branchId);

    //    return Ok(new
    //    {
    //        success = response.Success,
    //        message = response.Message,
    //        data = response.Data
    //    });
    //}



    //[HttpGet("All-Online-Orders/{branchId}")]
    //public async Task<IActionResult> GetAllOrders(
    //  int branchId,
    //  [FromQuery] DateTime? startDate = null,
    //  [FromQuery] DateTime? endDate = null,
    //  [FromQuery] DateTime? dateorder = null,
    //  [FromQuery] int? status = null,
    //  [FromQuery] OrderType? ordertyp = null,
    //  [FromQuery] int? orderId = null,
    //  [FromQuery] int? fromprice = null,
    //  [FromQuery] int? toprice = null,
    //  [FromQuery] int? pageNumber = null,
    //  [FromQuery] int? pageSize = null)
    //{
    //    try
    //    {
    //        var orderSpec = new OnlineOrderSpecification(branchId, orderId, dateorder, startDate, endDate, status, ordertyp, fromprice, toprice, pageNumber, pageSize);
    //        var orders = await _unitOfWork.orderRepo.GetAllOnlineOrdersAsync(orderSpec);
    //        var totalCount = await _unitOfWork.orderRepo.CountOnlineOrdersAsync(orderSpec);
    //        var orderDtos = orders.Select(order => new DashboardOrderDto(order)
    //        {
    //            Items = order.OrderItems?.Select(oi => new DashboardOrderItemDto(oi)
    //            {
    //            }).ToList()
    //        }).ToList();
    //        int totalPages = 1;
    //        if (pageSize.HasValue && pageNumber.HasValue)
    //            totalPages = (int)Math.Ceiling((double)totalCount / pageSize.Value);
    //        return Ok(new
    //        {
    //            Success = true,
    //            Data = orderDtos,
    //            TotalCount = totalCount,
    //            PageNumber = pageNumber,
    //            PageSize = pageSize,
    //            TotalPages = totalPages
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, $"Internal server error: {ex}");
    //    }
   // }

    [HttpGet("SalesReport")]
    public async Task<ActionResult> SalesReport(
        int branchId,
      [FromQuery] DateTime? startDate = null,
      [FromQuery] DateTime? endDate = null,
      [FromQuery] DateTime? dateorder = null,
      [FromQuery] int? status = null,
      [FromQuery] OrderType? ordertype = null,
      [FromQuery] int? orderId = null,
      [FromQuery] int? fromprice = null,
      [FromQuery] int? toprice = null,
      [FromQuery] string? paidstatus = null,
      [FromQuery] int? pageNumber = null,
      [FromQuery] int? pageSize = null)
    {
        //var orderSpec = new OnlineOrderSpecification(branchId, orderId, dateorder, startDate, endDate, status, ordertype, fromprice, toprice);
        //var orders = await _unitOfWork.orderRepo.GetAllOnlineOrdersAsync(orderSpec);
        //var totalCount = await _unitOfWork.orderRepo.CountOnlineOrdersAsync(orderSpec);
        //var orderSpec_ = new POSOrderSpecification(branchId, orderId, dateorder, ordertype, startDate, endDate, status, fromprice, toprice);
        //var orders_ = await _unitOfWork.posOrders.GetAllPOSOrdersAsync(orderSpec_);
        //var totalCount_ = await _unitOfWork.posOrders.CountPOSOrdersAsync(orderSpec_);
        //int totalcountt = totalCount + totalCount_;
        //var orderDtos = orders.Select(order => new DashboardOrderDto(order)
        //{
        //    paymentstatus = (PaymentStatus)1,
        //    Items = order.OrderItems?.Select(oi => new DashboardOrderItemDto(oi)
        //    {
        //    }).ToList()
        //}).ToList();
        //orderDtos.AddRange(orders_.Select(p => new DashboardOrderDto(p)
        //{
        //    Items = p.OrderItems.Select(l => new DashboardOrderItemDto(l)
        //    {

        //    }).ToList()
        //}).ToList());
        if (!pageNumber.HasValue)
        {
            return Ok(new
            {
                Success = true,
                //Data = orderDtos,
                //countofonlinorder = totalCount,
                //countofposorder = totalCount_,
                //TotalCount = totalcountt,
                PageNumber = 1,
                //PageSize = totalcountt,
                TotalPages = 1,
            });
        }
        else
        {
            pageSize = 10;
            //var orderDtoss = orderDtos.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            //int totalPages = (int)Math.Ceiling((double)totalcountt / pageSize.Value);
            return Ok(new
            {
                Success = true,
                //Data = orderDtoss,
                //countofonlinorder = totalCount,
                //countofposorder = totalCount_,
                //TotalCount = totalcountt,
                PageNumber = pageNumber,
                //PageSize = orderDtoss.Count(),
                //TotalPages = totalPages,
            });
        }
    }




    //[HttpGet("SalesReport")]
    //public async Task<IActionResult> SalesReport(
    //int branchId,
    //[FromQuery] DateTime? startDate = null,
    //[FromQuery] DateTime? endDate = null,
    //[FromQuery] DateTime? dateOrder = null,
    //[FromQuery] int? status = null,
    //[FromQuery] OrderType? orderType = null,
    //[FromQuery] int? orderId = null,
    //[FromQuery] int? fromPrice = null,
    //[FromQuery] int? toPrice = null,
    //[FromQuery] string? paidStatus = null,
    //[FromQuery] int? pageNumber = null,
    //[FromQuery] int? pageSize = null)
    //{
    //    var response = await _POSOrderAPPService.GetSalesReportAsync(
    //        branchId, startDate, endDate, dateOrder, status, orderType, orderId, fromPrice, toPrice, paidStatus, pageNumber, pageSize);

    //    return Ok(new
    //    {
    //        success = response.Success,
    //        message = response.Message,
    //        data = response.Data
    //    });
    //}





    ////    httpGet("All-Online-Orders")]
    ////public async Task<IActionResult> GetAllOrders(int branchid)
    ////{
    ////    try
    ////    {
    ////        var orders = await _orderRepository.GetAllOnlineOrdersAsync(branchid);
    ////        var orderDtos = orders?.Select(order => new DashboardOrderDto
    ////        {
    ////            Id = order.Id,
    ////            OrderDate = order.CreatedAt,
    ////            Status = order.Status,
    ////            TotalAmount = order.TotalAmount,
    ////            OrderType = order.Ordertype,
    ////            PaymentMethod = order.paymentMethod,
    ////            Discount = order.Discount,
    ////            employeeName = order.Customer?.fullName,
    ////            BranchId = order.BranchId,
    ////            BranchName = order.Branch?.Name,
    ////            Items = order.OrderItems?.Select(oi => new DashboardOrderItemDto
    ////            {
    ////                ItemId = oi.ItemId,
    ////                Quantity = oi.Quantity,
    ////                Price = oi.Price,
    ////                TotalPrice = oi.TotalPrice,
    ////                ItemName = _unitOfWork.Items.GetById(oi.ItemId).Name,
    ////                Variations = oi.SelectedVariations?.Select(v => new DashboardOrderItemVariationDto
    ////                {
    ////                    VariationName = v.VariationName,
    ////                    AttributeName = v.Attributename,
    ////                    AdditionalPrice = v.AdditionalPrice
    ////                }).ToList(),
    ////                Addons = oi.SelectedAddons?.Select(a => new DashboardOrderItemAddonDto
    ////                {
    ////                    AddonName = a.AddonName,
    ////                    AdditionalPrice = a.AdditionalPrice
    ////                }).ToList(),
    ////                Extras = oi.SelectedExtras?.Select(e => new DashboardOrderItemExtraDto
    ////                {
    ////                    ExtraName = e.ExtraName,
    ////                    AdditionalPrice = e.AdditionalPrice
    ////                }).ToList()
    ////            }).ToList()
    ////        }).ToList();

    ////        return Ok(new
    ////        {
    ////            success = true,
    ////            data = orderDtos
    ////        });
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        // Log the exception (ex)
    ////        return StatusCode(500, $"Internal server error{ex}");
    ////    }
    ////}
    //[HttpGet("OrderStatus/counts")]
    //public async Task<IActionResult> GetOrderCountsAndAllOrders()
    //{
    //    var statusCounts = new List<object>();
    //    int totalOrderCount = 0;
    //    foreach (OrderStatus status in Enum.GetValues(typeof(OrderStatus)))
    //    {
    //        var count = await _unitOfWork.Orders.CountAsync(new OrderSpecification(status));
    //        statusCounts.Add(new
    //        {
    //            Status = status.ToString(),
    //            Count = count
    //        });
    //        totalOrderCount += count;
    //    }
    //    var totalCountObject = new
    //    {
    //        Status = "Total Orders",
    //        Count = totalOrderCount
    //    };
    //    statusCounts.Insert(0, totalCountObject);
    //    return Ok(new
    //    {
    //        success = true,
    //        data = statusCounts

    //    });
    //}
    //[HttpGet("Getallcountordersbynumberofdays")]
    //public async Task<ActionResult> GetOrderCountsAndAllOrdersbydays(int days, int branchid)
    //{
    //    if (days <= 0)
    //    {
    //        return Ok(new { success = false, message = "The number of days should be greater than zero." });
    //    }
    //    var statusCounts = new List<object>();
    //    int totalOrderCount = 0;
    //    foreach (OrderStatus status in Enum.GetValues(typeof(OrderStatus)))
    //    {
    //        var count = await _unitOfWork.Orders.CountAsync(new OrderSpecification(status, days, branchid));
    //        statusCounts.Add(new
    //        {
    //            Status = status.ToString(),
    //            Count = count
    //        });
    //        totalOrderCount += count;
    //    }
    //    var totalCountObject = new
    //    {
    //        Status = "Total Orders",
    //        Count = totalOrderCount
    //    };
    //    statusCounts.Insert(0, totalCountObject);
    //    return Ok(new
    //    {
    //        success = true,
    //        data = statusCounts
    //    });
    //}
    //// Endpoint to get order by ID
    //[HttpGet("OnlineOrder{id}")]
    //public async Task<IActionResult> GetOrderById(int id)
    //{
    //    try
    //    {
    //        var order = await _unitOfWork.orderRepo.GetOrderByIdAsync(id);
    //        if (order == null)
    //        {
    //            return Ok(new { success = false, message = "Order not found." });
    //        }
    //        var orderDto = new DashboardOrderDto(order)
    //        {
    //            Items = order.OrderItems?.Select(oi => new DashboardOrderItemDto(oi)
    //            {

    //            }).ToList()
    //        };
    //        return Ok(new
    //        {
    //            success = true,
    //            data = orderDto
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception (ex)
    //        return StatusCode(500, "Internal server error");
    //    }
    //}


    ////[HttpGet("{id}")]
    ////public async Task<IActionResult> GetOrderById(int id)
    ////{
    ////    var order = await _orderRepository.GetByIdAsync(id);
    ////    if (order == null)
    ////    {
    ////        return NotFound();
    ////    }
    ////    return Ok(order);
    ////}
    //[HttpPut("EditReview")]
    //[Authorize]
    //public async Task<ActionResult> ReviewOrder(int count, int orderid)
    //{
    //    string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    WajbaUser customer = await ValidateTokenAndGetUser(token);
    //    if (customer == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    }
    //    Order order = await _unitOfWork.Orders.GetByIdAsync(orderid);
    //    if (order == null)
    //    {
    //        return Ok(new { success = false, message = "Order not found." });
    //    }
    //    if (order.CustomerId != customer.Id)
    //    {
    //        return Ok(new { success = false, message = "You do not have permission to delete this order." });
    //    }
    //    if (order.IsEditing)
    //        return Ok(new { success = false, message = "You do cannot review order." });
    //    order.Review = count;
    //    order.IsEditing = true;
    //    //order.UpdatedAt = DateTime.Now;
    //    await _unitOfWork.orderRepo.UpdateAsync(order);
    //    await _unitOfWork.CompleteAsync();
    //    return Ok(new { success = true, message = "the order reviewed " });
    //}
    //[HttpGet("Getreview")]
    //[Authorize]
    //public async Task<ActionResult> GetReviewOrder(int orderid)
    //{
    //    var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    var customer = await ValidateTokenAndGetUser(token);
    //    if (customer == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    }
    //    var order = await _unitOfWork.Orders.GetByIdAsync(orderid);
    //    if (order == null)
    //    {
    //        return Ok(new { success = false, message = "Order not found." });
    //    }
    //    if (order.CustomerId != customer.Id)
    //    {
    //        return Ok(new { success = false, message = "You do not have permission to delete this order." });
    //    }
    //    if (order.IsEditing)
    //        return Ok(new { success = false, message = "the order not reviewed before" });
    //    return Ok(new { success = true, message = order.Review });
    //}
    //[HttpPost]
    //public async Task<IActionResult> AddOrder([FromBody] OrderDTO orderDto)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return Ok(new { success = false, message = ModelState });
    //    }
    //    var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    if (string.IsNullOrEmpty(token))
    //    {
    //        return Ok(new { success = false, message = "Token is required" });
    //    }
    //    var customer = await ValidateTokenAndGetUser(token);
    //    if (customer == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    }
    //    var existingCart = await _unitOfWork.carts.GetCartByCustomerIdAsync(customer.Id);
    //    if (existingCart == null || !existingCart.CartItems.Any())
    //    {
    //        return Ok(new { success = false, message = "Customer Cart is empty" });
    //    }
    //    var branch = await _unitOfWork.Branches.GetByIdAsync(orderDto.BranchId);
    //    if (branch == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid BranchId." });
    //    }
    //    var order = new Order()
    //    {
    //        Status = (OrderStatus)orderDto.Status,
    //        Ordertype = (OrderType)orderDto.Ordertype,
    //        paymentMethod = (PaymentMethod)orderDto.paymentMethod,
    //        BranchId = orderDto.BranchId,
    //        CustomerId = customer.Id,
    //        Discount = existingCart.DiscountAmount,
    //        TotalAmount = existingCart.TotalAmount,
    //        OrderItems = new List<OrderItem>()
    //    };

    //    foreach (var cartItem in existingCart.CartItems)
    //    {
    //        var orderItem = new OrderItem(cartItem)
    //        {
    //            //BranchId = orderDto.BranchId,
    //        };
    //        order.OrderItems.Add(orderItem);
    //    }
    //    await _unitOfWork.Orders.AddAsync(order);
    //    var orderType = (OrderType)orderDto.Ordertype;
    //    switch (orderType)
    //    {
    //        case OrderType.Delivery:
    //            var deliveryOrder = new DeliveryOrder
    //            {
    //                Title = orderDto.DeliveryOrder.Title,
    //                Longitude = orderDto.DeliveryOrder.Longitude,
    //                Latitude = orderDto.DeliveryOrder.Latitude,
    //                ApproximateTime = orderDto.DeliveryOrder.ApproximateTime,
    //                Order = order
    //            };
    //            await _unitOfWork.DeliveryOrders.AddAsync(deliveryOrder);

    //            break;
    //        case OrderType.DineIn:
    //            var dineInOrder = new DineInOrder
    //            {
    //                Time = SettingFile.TryParseTime(orderDto.DineInOrder.Time),
    //                NumberOfPersons = orderDto.DineInOrder.NumberOfPersons,
    //                Date = SettingFile.TryParseDate(orderDto.DineInOrder.Date),
    //                Order = order
    //            };
    //            await _unitOfWork.DineInOrders.AddAsync(dineInOrder);

    //            break;
    //        case OrderType.PickUp:
    //            var pickupOrder = new PickUpOrder
    //            {
    //                Time = SettingFile.TryParseTime(orderDto.PickUpOrder.Time),
    //                Order = order
    //            };
    //            await _unitOfWork.PickUpOrders.AddAsync(pickupOrder);

    //            break;
    //        case OrderType.DriveThru:
    //            var driveThruOrder = new DriveThruOrder
    //            {
    //                Time = SettingFile.TryParseTime(orderDto.DriveThruOrder.Time),
    //                Date = SettingFile.TryParseDate(orderDto.DriveThruOrder.Date),
    //                CarType = orderDto.DriveThruOrder.CarType,
    //                CarNumber = orderDto.DriveThruOrder.CarNumber,
    //                CarColor = orderDto.DriveThruOrder.CarColor,
    //                Order = order
    //            };
    //            await _unitOfWork.DriveThruOrders.AddAsync(driveThruOrder);

    //            break;
    //        case OrderType.PosOrder:
    //            var posOrder = new PosOrder
    //            {
    //                PhoneNumber = orderDto.posOrder.PhoneNumber,
    //                TokenNumber = orderDto.posOrder.TokenNumber
    //            };
    //            await _unitOfWork.posOrders.AddAsync(posOrder);
    //            break;
    //        case OrderType.PosDelivery:
    //            var posDeliveryOrder = new PosDeliveryOrder()
    //            {

    //            };
    //            await _unitOfWork.posDeliveryOrders.AddAsync(posDeliveryOrder);
    //            break;
    //        default:
    //            return Ok(new { success = false, message = "Invalid order type." });
    //    }
    //    existingCart.CartItems.Clear();
    //    await _unitOfWork.carts.RemoveAsync(existingCart);
    //    await _unitOfWork.CompleteAsync();
    //    // Send the full order to the dashboard
    //    var hubContext = HttpContext.RequestServices.GetService<IHubContext<OrderHub>>();
    //    var fullOrder = new
    //    {
    //        Id = order.Id,
    //        Status = order.Status,
    //        Ordertype = order.Ordertype,
    //        TotalAmount = order.TotalAmount,
    //        Items = order.OrderItems.Select(i => new { i.Item.Name, i.Quantity, i.Price }),
    //        CustomerName = customer.fullName,
    //        BranchName = branch.Name
    //    };


    //    await hubContext.Clients.All.SendAsync("OrderReceived", fullOrder);

    //    return Ok(new { success = true, message = "order created" });
    //}

    //[HttpPut("update-order-status")]
    //public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
    //{
    //    var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
    //    if (order == null)
    //        return NotFound(new { success = false, message = "Order not found." });

    //    order.Status = Enum.Parse < OrderStatus > (status);
    //    await _unitOfWork.Orders.UpdateAsync(order);
    //    await _unitOfWork.CompleteAsync();

    //    var hubContext = HttpContext.RequestServices.GetService<IHubContext<OrderHub>>();

    //    // Notify all clients (dashboard) about the status update
    //    await hubContext.Clients.All.SendAsync("OrderUpdated", orderId, status);

    //    // Notify the specific customer about their order
    //    await hubContext.Clients.User(order.CustomerId.ToString())
    //        .SendAsync("OrderStatusChanged", orderId.ToString(), status);

    //    return Ok(new { success = true, message = "Order status updated." });
    //}

    //[HttpPut("update-order-status")]
    //public async Task<IActionResult> UpdateOrderStatus(int orderId, string status, [FromServices] IHubContext<OrderHub> hubContext)
    //{
    //    var response = await _POSOrderAPPService.UpdateOrderStatusAsync(orderId, status, hubContext);

    //    return Ok(new
    //    {
    //        success = response.Success,
    //        message = response.Message
    //    });
    //}


    [HttpPut("Kitchen-update-order-status")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, string status/*, [FromServices] IHubContext<OrderHub> hubContext*/)
    {





        try
        {
            var response = await _POSOrderAPPService.UpdateOrderStatusAsync(orderId, status/*, hubContext*/);
            return Ok(new ApiResponse<OrderDTO>
            {
                Success = true,
                Message = "Order status Updated successfully.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = $"Error Updating Order status: {ex.Message}",
                Data = null
            });
        }






        //return Ok(new
        //{
        //    success = response.Success,
        //    message = response.Message
        //});

    }



    //[HttpDelete("delete-order/{orderId}")]
    //[Authorize]
    //public async Task<IActionResult> DeleteOrder(int orderId)
    //{
    //    // Get token from headers
    //    var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    if (string.IsNullOrEmpty(token))
    //    {
    //        return Ok(new { success = false, message = "Token is required" });
    //    }

    //    // Validate token and get customer
    //    var customer = await ValidateTokenAndGetUser(token);
    //    if (customer == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid token or customer not found" });
    //    }

    //    // Fetch the order to check if it belongs to the customer
    //    var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
    //    if (order == null)
    //    {
    //        return Ok(new { success = false, message = "Order not found." });
    //    }

    //    // Check if the order belongs to the customer
    //    if (order.CustomerId != customer.Id)
    //    {
    //        return Ok(new { success = false, message = "You do not have permission to delete this order." });
    //    }

    //    // Delete the order
    //    //await _orderRepository.RemoveAsync(order); 
    //    await _unitOfWork.Orders.RemoveAsync(order);
    //    await _unitOfWork.CompleteAsync();
    //    return Ok(new { success = true, message = "Order deleted successfully." });
    //}

    //[HttpDelete("delete-order/{orderId}")]
    ////[Authorize]
    //public async Task<IActionResult> DeleteOrder(int orderId)
    //{
    //    var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    //    var response = await _POSOrderAPPService.DeleteOrderAsync(orderId, token);

    //    return Ok(new
    //    {
    //        success = response.Success,
    //        message = response.Message
    //    });
    //}

    //private async Task<ApplicationUser> ValidateTokenAndGetUser(string token)
    //{
    //    var principal = GetPrincipalFromToken(token);
    //    if (principal == null)
    //    {
    //        return null;
    //    }
    //    var userIdClaim = principal.FindFirst("Id");
    //    if (userIdClaim == null || principal.FindFirst("UserType").Value != "Customer")
    //    {
    //        return null;
    //    }
    //    var userId = userIdClaim.Value;
    //    return await _userManager.FindByIdAsync(userId);
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

}
