﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Wajba.Enums;

namespace Wajba.CouponContract
{
    public class CouponDto : EntityDto<int>
    {
        public string Name { get; set; }
        public decimal Code { get; set; }
        public decimal Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal MinimumOrderAmount { get; set; }
        public decimal MaximumDiscount { get; set; }
        public int LimitPerUser { get; set; }
        public int CountOfUsers { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public int BranchId { get; set; }
        public bool IsExpired { get; set; }
    }
}
