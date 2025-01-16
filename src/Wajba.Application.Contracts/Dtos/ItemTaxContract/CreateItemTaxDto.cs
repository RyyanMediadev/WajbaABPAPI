namespace Wajba.Dtos.ItemTaxContract;

public class CreateItemTaxDto
{
    public string Name { get; set; }
    public decimal Code { get; set; }
    public int TaxRate { get; set; }
    public int Status { get; set; }
}
