﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Wajba.CouponsDomain;
using Wajba.Items;
using Wajba.OfferDomain;

namespace Wajba.BranchDomain
{
    public class Branch : FullAuditedEntity<int>
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
       // public int CompanyId { get; set; }
       // public virtual Company? Company { get; set; }
        public virtual ICollection<ItemBranch> ItemBranches { get; set; } = new List<ItemBranch>();
      //  public ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
        public ICollection<Offer>? Offers { get; set; }
        public ICollection<Coupon>? Coupons { get; set; }
    }
}
