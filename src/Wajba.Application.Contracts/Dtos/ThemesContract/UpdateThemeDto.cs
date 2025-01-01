namespace Wajba.Dtos.ThemesContract;

public class UpdateThemeDto
{
    public int Id { get; set; }
    [Required]
    public IFormFile LogoUrl { get; set; }
    [Required]
    public IFormFile BrowserTabIconUrl { get; set; }
    [Required]
    public IFormFile FooterLogoUrl { get; set; }
}
