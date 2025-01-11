using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Mapping
{
    public class ItemTaxMappingProfile:Profile
    {
        public ItemTaxMappingProfile()
        {
            CreateMap<ItemTax, ItemTaxDto>().ReverseMap();
            CreateMap<CreateItemTaxDto, ItemTax>().ReverseMap();
            CreateMap<UpdateItemTaxDto, ItemTax>().ReverseMap();
        }
    }
}
