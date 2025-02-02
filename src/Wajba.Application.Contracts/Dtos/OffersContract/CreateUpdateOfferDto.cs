﻿namespace Wajba.Dtos.OffersContract;

public class CreateUpdateOfferDto
{
    public string Name { get; set; }
    public int Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal DiscountPercentage { get; set; }
    public int DiscountType { get; set; }
    public Base64ImageModel Model { get; set; }
    public string Description { get; set; }
    public int BranchId { get; set; }

    public List<int>? ItemIds { get; set; } = new List<int>();

    public List<int>? CategoryIds { get; set; } = new List<int>();
}