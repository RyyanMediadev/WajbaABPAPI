using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.ItemExtraContract
{
    public class ItemExtraDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public decimal AdditionalPrice { get; set; }
        public int ItemId { get; set; }
    }
}
