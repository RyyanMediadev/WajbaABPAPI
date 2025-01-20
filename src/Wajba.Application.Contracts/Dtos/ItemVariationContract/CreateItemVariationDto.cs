namespace Wajba.Dtos.ItemVariationContract;

public class CreateItemVariationDto
{
    public string Name { get; set; }
    public string Note { get; set; }
    public int Status { get; set; }
    public decimal AdditionalPrice { get; set; }
    public int ItemAttributesId { get; set; }
    public int ItemId { get; set; }
}
public class UpdateItemVariationDto : CreateItemVariationDto
{
    public int VariationId { get; set; }
}
