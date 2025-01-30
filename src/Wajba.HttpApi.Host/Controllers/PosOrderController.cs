
using Fos_EF.AddSpecification;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wajba.Controllers;
using Wajba.Dtos.OrderContract;
using Wajba.Enums;
using Wajba.Models.Orders;
using Wajba.Models.OrdersDomain;
using Wajba.Models.WajbaUserDomain;
using Wajba.Settings;
using Wajba.WajbaUsersService;

namespace FosAPI.Controllers;
//[Authorize]

public class PosOrderController : WajbaController
{
    //private readonly IUnitOfWork _unitOfWork;
    private readonly WajbaUsersAppservice userManager;
    private readonly POSOrderAPPService _POSOrderAPPService;
    private readonly IConfiguration _configuration;
    public PosOrderController(
        WajbaUsersAppservice _userManager, IConfiguration configuration)
    {
        //_unitOfWork = unitOfWork;
        userManager = _userManager;
        _configuration = configuration;
    }

    //[HttpGet("employee-orders")]
    //public async Task<IActionResult> GetAllOrdersForemployee()
    //{
    //    var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //    if (string.IsNullOrEmpty(token))
    //    {
    //        return Ok(new { success = false, message = "Token is required" });
    //    }
    //    var employee = await ValidateTokenAndGetUser(token);
    //    if (employee == null)
    //    {
    //        return Ok(new { success = false, message = "Invalid token or employee not found" });
    //    }

    //    // Fetch all orders for the specific employee
    //    var orders = await _orderRepository.GetOrdersByEmployeeIdAsync(employee.Id);

    //    if (orders == null || !orders.Any())
    //    {
    //        return Ok(new { success = false, message = "No orders found for this employee." });
    //    }

    //    // Return the orders
    //    return Ok(new { success = true, orders });
    //}

    [HttpGet("All-POS-Orders/{branchId}")]
    public async Task<IActionResult> GetAllOrders(
   int branchId,
   [FromQuery] DateTime? startDate = null,
   [FromQuery] int? orderid = null,
   [FromQuery] OrderType? orderType = null,
   [FromQuery] DateTime? endDate = null,
   [FromQuery] DateTime? dateorder = null,
   [FromQuery] int? status = null,
   [FromQuery] int? fromprice = null,
   [FromQuery] int? toprice = null,
   [FromQuery] int? pageNumber = null,
   [FromQuery] int? pageSize = null)
    {
        try
        {
            var orderSpec = new POSOrderSpecification(branchId, orderid, dateorder, orderType, startDate, endDate, status, fromprice, toprice, pageNumber, pageSize);



            // var orders = await _unitOfWork.posOrders.GetAllPOSOrdersAsync(orderSpec);
            var orders = _POSOrderAPPService.GetAllPOSOrdersAsync(orderSpec);


            //var totalCount = await _unitOfWork.posOrders.CountPOSOrdersAsync(orderSpec);

            var totalCount = _POSOrderAPPService.CountPOSOrdersAsync(orderSpec);


            var orderDtos = orders.Select(order => new DashboardOrderDto(order)
            {
                Items = order.OrderItems?.Select(oi => new DashboardOrderItemDto(oi)
                {
                }).ToList()
            }).ToList();
            int totalPages = 1;
            if (pageSize.HasValue && pageNumber.HasValue)
            {
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize.Value);
            }
            return Ok(new
            {
                Success = true,
                Data = orderDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }
    [HttpGet("PosOrder{id}")]
    //public async Task<IActionResult> GetOrderById(int id)
    //{
    //    try
    //    {
    //        //var order = await _unitOfWork.orderRepo.GetOrderByIdAsync(id);

    //        var order = _POSOrderAPPService.GetOrderByIdAsync(id);

    //        if (order == null)
    //        {
    //            return Ok(new { success = false, message = "Order not found." });
    //        }
    //        var orderDto = new DashboardOrderDto()
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
    //        return StatusCode(500, $"Internal server error{ex}");
    //    }
    //}

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        //var order = await _orderRepository.GetByIdAsync(id);
        //if (order == null)
        //{
        //    return NotFound();
        //}
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder(OrderDTO orderDto)
    {

        if (!ModelState.IsValid)
        {
            return Ok(new { success = false, message = ModelState });
        }
        // Get token from headers
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (string.IsNullOrEmpty(token))
        {
            return Ok(new { success = false, message = "Token is required" });
        }
        var employee = await _POSOrderAPPService.ValidateTokenAndGetUser(token);
        if (employee == null)
        {
            return Ok(new { success = false, message = "Invalid token or employee not found" });
        }
        //var existingCart = await _unitOfWork.carts.GetCartByEmployeeIdAsync(employee.Id);

        var existingCart = _POSOrderAPPService.GetCartByEmployeeIdAsync(employee.Id); ;

        if (existingCart == null )
        {
            return Ok(new { success = false, message = "employee Cart is empty" });
        }
        var branch = await _POSOrderAPPService.GetOrderByIdAsync(orderDto.BranchId);
        if (branch == null)
        {
            return Ok(new { success = false, message = "Invalid BranchId." });
        }
        var order = new Order()
        {
            Status = (OrderStatus)orderDto.Status,
            Ordertype = (OrderType)orderDto.Ordertype,
            paymentMethod = (PaymentMethod)orderDto.paymentMethod,
            BranchId = orderDto.BranchId,
            userId = employee.Id,
           // Discount = existingCart.DiscountAmount,
           // TotalAmount = existingCart.TotalAmount,
            OrderItems = new List<OrderItem>()
        };

        //foreach (var cartItem in existingCart)
        //{
        //    var orderItem = new OrderItem()
        //    {
        //    };
        //    order.OrderItems.Add(orderItem);
        //}
        ////await _unitOfWork.Orders.AddAsync(order);

        //await _POSOrderAPPService.AddAsync(order);
        ////await _unitOfWork.CompleteAsync();

        //var orderType = (OrderType)orderDto.Ordertype;
        //switch (orderType)
        //{
        //    case OrderType.Delivery:
        //        var deliveryOrder = new DeliveryOrder()
        //        {
        //            Order = order,
        //        };
        //        //await _unitOfWork.DeliveryOrders.AddAsync(deliveryOrder);
        //        await _POSOrderAPPService.AddAsync(deliveryOrder);


        //        break;
        //    case OrderType.DineIn:
        //        var dineInOrder = new DineInOrder
        //        {
        //            Time = SettingFile.TryParseTime(orderDto.DineInOrder.Time),
        //            NumberOfPersons = orderDto.DineInOrder.NumberOfPersons,
        //            Date = SettingFile.TryParseDate(orderDto.DineInOrder.Date),
        //            Order = order
        //        };
        //        //await _unitOfWork.DineInOrders.AddAsync(dineInOrder);

        //        await _POSOrderAPPService.AddAsync(dineInOrder);


        //        break;
        //    case OrderType.PickUp:
        //        var pickupOrder = new PickUpOrder
        //        {
        //            Time = SettingFile.TryParseTime(orderDto.PickUpOrder.Time),
        //            Order = order
        //        };
        //        //await _unitOfWork.PickUpOrders.AddAsync(pickupOrder);
        //        await _POSOrderAPPService.AddAsync(pickupOrder);

        //        break;
        //    case OrderType.DriveThru:
        //        var driveThruOrder = new DriveThruOrder
        //        {
        //            Time = SettingFile.TryParseTime(orderDto.DriveThruOrder.Time),
        //            Date = SettingFile.TryParseDate(orderDto.DriveThruOrder.Date),
        //            CarType = orderDto.DriveThruOrder.CarType,
        //            CarNumber = orderDto.DriveThruOrder.CarNumber,
        //            CarColor = orderDto.DriveThruOrder.CarColor,
        //            Order = order
        //        };
        //        //await _unitOfWork.DriveThruOrders.AddAsync(driveThruOrder);
        //        await _POSOrderAPPService.AddAsync(driveThruOrder);


        //        break;
        //    case OrderType.PosOrder:
        //        var posOrder = new PosOrder()
        //        {
        //            PhoneNumber = orderDto.posOrder.PhoneNumber,
        //            TokenNumber = orderDto.posOrder.TokenNumber
        //        };
        //        //await _unitOfWork.posOrders.AddAsync(posOrder);
        //        await _POSOrderAPPService.AddAsync(posOrder);


        //        break;
        //    case OrderType.PosDelivery:
        //        var posDeliveryOrder = new PosDeliveryOrder()
        //        {

        //        };
        //        //await _unitOfWork.posDeliveryOrders.AddAsync(posDeliveryOrder);
        //        await _POSOrderAPPService.AddAsync(posDeliveryOrder);

        //        break;
        //    default:
        //        return Ok(new { success = false, message = "Invalid order type." });
        //}
        ////await _unitOfWork.CompleteAsync();
        ////existingCart.CartItems.Clear();
        ////await _unitOfWork.carts.RemoveAsync(existingCart);
        ////await _unitOfWork.CompleteAsync();
        return Ok(new { success = true, message = "order created" });
    }

    [HttpDelete("delete-order/{orderId}")]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        // Get token from headers
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (string.IsNullOrEmpty(token))
        {
            return Ok(new { success = false, message = "Token is required" });
        }

        // Validate token and get employee
        //var employee = await ValidateTokenAndGetUser(token);
        //if (employee == null)
        //{
        //    return Ok(new { success = false, message = "Invalid token or employee not found" });
        //}

        // Fetch the order to check if it belongs to the employee
        ////var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
        //if (order == null)
        //{
        //    return Ok(new { success = false, message = "Order not found." });
        //}

        //// Check if the order belongs to the employee
        //if (order.userId != employee.Id)
        //{
        //    return Ok(new { success = false, message = "You do not have permission to delete this order." });
        //}
        ////await _unitOfWork.Orders.RemoveAsync(order);
        //await _unitOfWork.CompleteAsync();
        return Ok(new { success = true, message = "Order deleted successfully." });
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

}
