
using Fos_EF.AddSpecification;
using Microsoft.IdentityModel.Tokens;
using Polly;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Volo.Abp.EntityFrameworkCore;
using Wajba.Controllers;
using Wajba.Dtos.OrderContract;
using Wajba.Enums;
using Wajba.Models.Orders;
using Wajba.Models.OrdersDomain;
using Wajba.Models.WajbaUserDomain;
using Wajba.OTPService;
using Wajba.Settings;
using Wajba.WajbaUsersService;

namespace FosAPI.Controllers;
//[Authorize]

public class PosOrderController : WajbaController
{

    private readonly WajbaDbContext _context;


    //private readonly IUnitOfWork _unitOfWork;
    private readonly WajbaUsersAppservice userManager;
    private readonly POSOrderAPPService _POSOrderAPPService;
    private readonly IConfiguration _configuration;
    public PosOrderController(
        WajbaUsersAppservice _userManager, IConfiguration configuration, WajbaDbContext context, POSOrderAPPService POSOrderAPPService)
    {
        //_unitOfWork = unitOfWork;
        //userManager = _userManager;
        //_configuration = configuration
        _POSOrderAPPService = POSOrderAPPService;
        _context = context;

    }

    [HttpGet("employee-orders")]
    public async Task<IActionResult> GetAllOrdersForEmployee()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        var response = await _POSOrderAPPService.GetAllOrdersForEmployeeAsync(token);

        return Ok(new
        {
            success = response.Success,
            message = response.Message,
            data = response.Data
        });
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
    public async Task<IActionResult> GetAllOrders(   int branchId,   [FromQuery] DateTime? startDate = null,   [FromQuery] int? orderid = null,
   [FromQuery] OrderType? orderType = null,   [FromQuery] DateTime? endDate = null,   [FromQuery] DateTime? dateorder = null,
   [FromQuery] int? status = null,   [FromQuery] int? fromprice = null,   [FromQuery] int? toprice = null,
   [FromQuery] int? pageNumber = null,   [FromQuery] int? pageSize = null)
    {
        try
        {
            var orderSpec = new POSOrderSpecification(branchId, orderid, dateorder, orderType, startDate, endDate, status, fromprice, toprice, pageNumber, pageSize);



            // var orders = await _unitOfWork.posOrders.GetAllPOSOrdersAsync(orderSpec);
            var orderDtos = _POSOrderAPPService.GetAllPOSOrdersAsync(orderSpec);  // Safer blocking call;



            //var totalCount = await _unitOfWork.posOrders.CountPOSOrdersAsync(orderSpec);

            var totalCount = _POSOrderAPPService.CountPOSOrdersAsync(orderSpec);


            //var orderDtos = orders.Select(order => new DashboardOrderDto(order)
            //{
            //    Items = order.OrderItems?.Select(oi => new DashboardOrderItemDto(oi)
            //    {
            //    }).ToList()

            //}).ToList();



            int totalPages = 1;
            //if (pageSize.HasValue && pageNumber.HasValue)
            //{
            //    totalPages = (int)Math.Ceiling((double)totalCount / pageSize.Value);
            //}
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




    [HttpPost]
    public async Task<IActionResult> AddOrder(OrderDTO orderDto)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var response = await _POSOrderAPPService.AddOrderAsync(orderDto, token);



        return Ok(new { success = response.Success, message = response.Message, data = response.Data });
    }


    [HttpDelete("delete-order/{orderId}")]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var response = await _POSOrderAPPService.DeleteOrderAsync(orderId, token);

        return Ok(new { success = response.Success, message = response.Message });
    }

    [HttpGet("PosOrder/{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var response = await _POSOrderAPPService.GetOrderByIdAsync(id);
        return Ok(new { success = response.Success, message = response.Message, data = response.Data });




    }


}
