﻿namespace Wajba.Dtos.ItemTaxContract
{
    public class CreateUpdateItemTaxDto
    {
        public string Name { get; set; }
        public decimal Code { get; set; }
        public int TaxRate { get; set; }
        public Status Status { get; set; }
    }
}
