using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.Dtos.ItemAddonContract;
using Wajba.Dtos.ItemExtraContract;

namespace Wajba.Dtos.ItemsDtos.ItemDependencies
{
    public class ItemWithDependenciesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public bool IsFeatured { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal TaxValue { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ItemType { get; set; }
        public bool IsDeleted { get; set; }
        public List<int> BranchesIds { get; set; }
        public List<ItemAddonDTO> ItemAddons { get; set; }
        public List<ItemExtraDTO> ItemExtras { get; set; }
        public List<AttributeDto> Attributes { get; set; }
    }
    public class AttributeDto
    {
        public string AttributeName { get; set; }
        public List<VariationDTO> Variations { get; set; }
    }

    public class VariationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public decimal AdditionalPrice { get; set; }
        public int? ItemAttributesId { get; set; }
    }

    public class ItemAddonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AdditionalPrice { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ItemExtraDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AdditionalPrice { get; set; }
    }
}
