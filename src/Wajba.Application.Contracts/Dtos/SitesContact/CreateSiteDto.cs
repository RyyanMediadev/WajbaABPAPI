namespace Wajba.Dtos.SitesContact;

public class CreateSiteDto
{
    [Required]
    public string Name { get; set; }
    [Required,EmailAddress]
    public string Email { get; set; }
    [Required,Url]
    public string iosappLink { get; set; }
    [Required,Url]
    public string androidAPPLink { get; set; }
    [Required]
    public string Copyrights { get; set; }
    [Required]
    public string googleMapKey { get; set; }
    [Required]
    public int digitAfterDecimal { get; set; }
    [Required]
    public int currencyPosition { get; set; }
    [Required]
    public int languageSwitch { get; set; }
    [Required]
    public int defaultBranch { get; set; }
    [Required]
    public int defaultCurrency { get; set; }
    [Required]
    public int defaultLanguage { get; set; }
}
