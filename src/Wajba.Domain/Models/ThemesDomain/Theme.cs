namespace Wajba.Models.ThemesDomain;

public class Theme : FullAuditedEntity<int>
{
    public string LogoUrl { get; set; }
    public string BrowserTabIconUrl { get; set; }
    public string FooterLogoUrl { get; set; }
    public Theme()
    {

    }
}