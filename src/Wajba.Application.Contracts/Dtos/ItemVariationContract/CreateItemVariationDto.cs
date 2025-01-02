using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.ItemVariationContract
{
    public class CreateItemVariationDto
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public Status Status { get; set; }
        public decimal AdditionalPrice { get; set; }
        public int ItemAttributesId { get; set; }
        public int ItemId { get; set; }
    }
    public class UpdateItemVariationDto : CreateItemVariationDto
    {
    }
}
