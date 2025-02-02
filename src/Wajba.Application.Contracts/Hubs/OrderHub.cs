using Microsoft.AspNetCore.SignalR;

namespace Wajba.Hubs;

public class OrderHub:Hub
    {
        public async Task SendOrderToDashboard(object order)
        {
            // Notify all connected admin clients about the new order
            await Clients.All.SendAsync("OrderReceived", order);
        }

        public async Task NotifyCustomer(string customerId, string orderId, string status)
        {
            // Notify a specific customer about their order status
            await Clients.User(customerId).SendAsync("OrderStatusChanged", orderId, status);
        }
    }

