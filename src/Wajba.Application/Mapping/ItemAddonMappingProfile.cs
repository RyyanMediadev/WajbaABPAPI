using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.Dtos.ItemAddonContract;
using Wajba.Models.ItemAddonDomain;

namespace Wajba.Mapping
{
    public class ItemAddonMappingProfile : Profile
    {
        public ItemAddonMappingProfile()
        {
            CreateMap<ItemAddon, ItemAddonDto>();
            CreateMap<CreateUpdateItemAddonDto, ItemAddon>();
        }
    }
}
