namespace Wajba.Dtos.ItemAddonContract;

public class CreateUpdateItemAddonDto
{
    public string AddonName { get; set; }
    public decimal AdditionalPrice { get; set; }
    public int ItemId { get; set; }
}
