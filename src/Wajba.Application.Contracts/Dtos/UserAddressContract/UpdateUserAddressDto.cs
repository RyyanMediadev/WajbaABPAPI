using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.UserAddressContract
{
    public class UpdateUserAddressDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string CustomerId { get; set; }
        public string? BuildingName { get; set; }
        public string? Street { get; set; }
        public string? ApartmentNumber { get; set; }
        public string? Floor { get; set; }
        public string? AddressLabel { get; set; }
        public int AddressType { get; set; }
    }
}
