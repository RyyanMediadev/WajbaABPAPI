using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.ItemVariationContract
{
    public class ItemVariationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public decimal AdditionalPrice { get; set; }
        public int ItemAttributesId { get; set; }
        public int ItemId { get; set; }
    }
}
