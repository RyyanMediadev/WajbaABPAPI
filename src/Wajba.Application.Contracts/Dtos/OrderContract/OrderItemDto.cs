namespace Wajba.Dtos.OrderContract;

    public class OrderItemDto
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Instruction { get; set; }
        public List<OrderItemVariationDto> SelectedVariations { get; set; }
        public List<OrderItemAddonDto> SelectedAddons { get; set; }
        public List<OrderItemExtraDto> SelectedExtras { get; set; }
    }

    // DTO for variations
    public class OrderItemVariationDto
    {
        public string VariationName { get; set; }
        public string AttributeName { get; set; }
        public decimal AdditionalPrice { get; set; }
    }

    // DTO for addons
    public class OrderItemAddonDto
    {
        public string AddonName { get; set; }
        public decimal AdditionalPrice { get; set; }
    }

    // DTO for extras
    public class OrderItemExtraDto
    {
        public string ExtraName { get; set; }
        public decimal AdditionalPrice { get; set; }
    }

