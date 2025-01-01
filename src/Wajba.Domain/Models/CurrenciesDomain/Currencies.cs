namespace Wajba.Models.CurrenciesDomain;

public class Currencies : FullAuditedEntity<int>
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Code { get; set; }
    public double ExchangeRate { get; set; }
    public bool IsCryptoCurrency { get; set; }

    public Currencies()
    {

    }
}
