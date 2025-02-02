using Microsoft.AspNetCore.SignalR;

namespace FosAPI.Hubs;

public class SendOfferNotificationToCustomerHub:Hub
{
    public async Task SendOfferToClients(object offerNotification)
    {
        // Sends the structured offerNotification object to all connected clients
        await Clients.All.SendAsync("ReceiveOffer", offerNotification);
    }

    // Method to send offer to a specific client (by connection ID)
    public async Task SendOfferToClient(string connectionId, object offerNotification)
    {
        await Clients.Client(connectionId).SendAsync("ReceiveOffer", offerNotification);
    }
}
