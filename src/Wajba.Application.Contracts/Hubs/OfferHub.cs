
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.OffersContract;

namespace Wajba.Hubs
{
    public class OfferHub:Hub
    {
        public async Task SendOffer(OfferDto offer)
        {
            await Clients.All.SendAsync("ReceiveOffer", offer);
        }
    }
}
