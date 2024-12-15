﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Wajba.Enums;

namespace Wajba.OffersContract
{
    public class OfferDto : EntityDto<int>
    {
        public string Name { get; set; }
        public Status Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Image { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DiscountType DiscountType { get; set; }
        public string Description { get; set; }
        public int BranchId { get; set; }
    }
}
