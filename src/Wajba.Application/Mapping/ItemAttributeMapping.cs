using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Mapping
{
    public class ItemAttributeMapping:Profile
    {
        public ItemAttributeMapping()
        {
            CreateMap<ItemAttribute, ItemAttributeDto>().ReverseMap();
            CreateMap<CreateItemAttributeDto, ItemAttribute>().ReverseMap();
            CreateMap<UpdateItemAttributeDto, ItemAttribute>().ReverseMap();
        }
    }
}
