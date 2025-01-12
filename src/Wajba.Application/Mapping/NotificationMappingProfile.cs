using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wajba.Dtos.NotificationContract;
using Wajba.Models.NotificationDomain;


namespace Wajba.Mapping
{
    public class NotificationMappingProfile:Profile
    {
        public NotificationMappingProfile()
        {


            CreateMap<CreateNotificationDto,Notification>().ReverseMap();
            CreateMap<UpdateNotificationDto, Notification>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();

        }
    }
}
