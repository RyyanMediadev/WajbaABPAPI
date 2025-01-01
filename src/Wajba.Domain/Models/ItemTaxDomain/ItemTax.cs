namespace Wajba.Models.ItemTaxDomain;

public class ItemTax : FullAuditedEntity<int>
{
    public string Name { get; set; }
    public decimal Code { get; set; }
    public int TaxRate { get; set; }
    public Status Status { get; set; }
    public ItemTax()
    {

    }

}
