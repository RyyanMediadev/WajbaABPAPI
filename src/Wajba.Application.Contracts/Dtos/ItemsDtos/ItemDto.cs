global using System.Collections.Generic;
using Wajba.Dtos.ItemAddonContract;
using Wajba.Dtos.ItemExtraContract;
using Wajba.Dtos.ItemVariationContract;

namespace Wajba.Dtos.ItemsDtos;

public class ItemDto:FullAuditedEntityDto<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    public string status { get; set; }
    public bool IsFeatured { get; set; }
    public string imageUrl { get; set; }
    public decimal Price { get; set; }
    public decimal? TaxValue { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string ItemType { get; set; }
    public bool IsDeleted { get; set; }
    public List<int> Branchesids {  get; set; }
    //public List<BranchesDTO> Branches { get; set; } = new List<BranchesDTO>();
    public List<ItemAddonDto> ItemAddons { get; set; } = new List<ItemAddonDto>();
    public List<ItemExtraDto> ItemExtras { get; set; } = new List<ItemExtraDto>();
    public List<ItemVariationDto> ItemVariations { get; set; } = new List<ItemVariationDto>();
}
