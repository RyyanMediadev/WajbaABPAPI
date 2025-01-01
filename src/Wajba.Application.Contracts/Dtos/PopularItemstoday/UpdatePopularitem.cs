using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.PopularItemstoday
{
    public class UpdatePopularitem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public IFormFile? ImgFile { get; set; }  // Optional for updates (new image upload)
        public decimal PrePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public string Description { get; set; }
        public int BranchId { get; set; }
    }
}
