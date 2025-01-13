using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.ItemAddonContract
{
    public class UpdateItemAddonDto
    {
       
        public int itemId { get; set; }
        public int addonId { get; set; }
        public string AddonName { get; set; }
        public decimal AdditionalPrice { get; set; }
        public int ItemId { get; set; }
    }
}
