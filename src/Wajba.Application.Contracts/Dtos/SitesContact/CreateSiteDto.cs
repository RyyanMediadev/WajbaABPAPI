namespace Wajba.Dtos.SitesContact;

public class CreateSiteDto
{
    [Required]
    public string Name { get; set; }
    [Required,EmailAddress]
    public string Email { get; set; }
    [Required,Url]
    public string IOSAPPLink { get; set; }
    [Required,Url]
    public string AndroidAPPLink { get; set; }
    [Required]
    public string Copyrights { get; set; }
    [Required]
    public string GoogleMapKey { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public CurrencyPosition CurrencyPosition { get; set; }
    [Required]
    public LanguageSwitch LanguageSwitch { get; set; }
    [Required]
    public int BranchId { get; set; }
    [Required]
    public int CurrencyId { get; set; }
    [Required]
    public int LanguageId { get; set; }
}
