namespace Wajba.Dtos.SitesContact;

public class SiteDto:EntityDto<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string IOSAPPLink { get; set; }
    public string AndroidAPPLink { get; set; }
    public string Copyrights { get; set; }
    public string GoogleMapKey { get; set; }
    public int Quantity { get; set; }
    public int CurrencyPosition { get; set; }
    public int LanguageSwitch { get; set; }
    public int BranchId { get; set; }
    public int CurrencyId { get; set; }
    public int LanguageId { get; set; }
}
