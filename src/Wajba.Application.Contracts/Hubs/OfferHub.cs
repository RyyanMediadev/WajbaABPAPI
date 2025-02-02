using Microsoft.AspNetCore.SignalR;
using Wajba.Dtos.OffersContract;

namespace Wajba.Hubs;

public class OfferHub:Hub
{
    public async Task SendOffer(OfferDto offer)
    {
        await Clients.All.SendAsync("ReceiveOffer", offer);
    }
}
