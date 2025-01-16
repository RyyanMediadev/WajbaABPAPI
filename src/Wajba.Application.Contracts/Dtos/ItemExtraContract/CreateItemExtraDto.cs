namespace Wajba.Dtos.ItemExtraContract;

public class CreateItemExtraDto
{
    public string Name { get; set; }
    public Status Status { get; set; }
    public decimal AdditionalPrice { get; set; }
    public int ItemId { get; set; }
}
public class UpdateItemExtraDto : CreateItemExtraDto
{
    public int extraId { get; set; }
    public int itemId { get; set; }
}
