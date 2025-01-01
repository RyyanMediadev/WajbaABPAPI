using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.Dtos.ItemVariationContract;
using Wajba.Models.ItemVariationDomain;

namespace Wajba.Mapping
{
    public class ItemVariationMappingProfile:Profile
    {
        public ItemVariationMappingProfile()
        {
            CreateMap<ItemVariation, ItemVariationDto>();
            CreateMap<CreateItemVariationDto, ItemVariation>();
            CreateMap<UpdateItemVariationDto, ItemVariation>();
        }
    }
}
