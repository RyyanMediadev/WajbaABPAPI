using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Models.AddressDomain
{
    public class UserAddress:Entity<int>
    {
        public string Title { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string CustomerId { get; set; }
       // public virtual APPUser Customer { get; set; }
        public string? BuildingName { get; set; }
        public string? Street { get; set; }
        public string? ApartmentNumber { get; set; }
        public string? Floor { get; set; }
        public string? AddressLabel { get; set; }
        public EmployeeAddressType AddressType { get; set; }
    }
}
