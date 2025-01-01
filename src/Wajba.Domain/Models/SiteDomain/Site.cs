global using Wajba.Models.CurrenciesDomain;
global using Wajba.Models.LanguageDomain;

namespace Wajba.Models.SiteDomain;

public class Site : FullAuditedEntity<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string IOSAPPLink { get; set; }
    public string AndroidAPPLink { get; set; }
    public string Copyrights { get; set; }
    public string GoogleMapKey { get; set; }
    public int Quantity { get; set; }
    public CurrencyPosition currencyPosition { get; set; }
    public LanguageSwitch languageSwitch { get; set; }
    // Foreign key
    public int BranchId { get; set; }
    public int CurrencyId { get; set; }
    public int LanguageId { get; set; }

    // Navigation properties
    public virtual Branch? Branch { get; set; }
    public virtual Currencies? Currencies { get; set; }
    public virtual Language? Language { get; set; }

    public Site()
    {

    }
}