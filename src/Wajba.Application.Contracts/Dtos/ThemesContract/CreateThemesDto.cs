namespace Wajba.Dtos.ThemesContract;
public class CreateThemesDto
{
    [Required]
    public Base64ImageModel LogoUrl { get; set; }
    [Required]
    public Base64ImageModel BrowserTabIconUrl { get; set; }
    [Required]
    public Base64ImageModel FooterLogoUrl { get; set; }


}
