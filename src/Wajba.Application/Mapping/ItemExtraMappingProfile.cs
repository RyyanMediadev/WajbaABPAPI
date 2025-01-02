using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.Dtos.ItemExtraContract;
using Wajba.Models.ItemExtraDomain;

namespace Wajba.Mapping
{
    public class ItemExtraMappingProfile:Profile
    {
        public ItemExtraMappingProfile()
        {
            CreateMap<ItemExtra, ItemExtraDto>();
            CreateMap<CreateItemExtraDto, ItemExtra>();
            CreateMap<UpdateItemExtraDto, ItemExtra>();
        }
    }
}
