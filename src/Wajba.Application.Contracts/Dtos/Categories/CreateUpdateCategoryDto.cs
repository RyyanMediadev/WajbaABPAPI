global using Microsoft.AspNetCore.Http;

namespace Wajba.Dtos.Categories;

public class CreateUpdateCategoryDto
{
    [Required]
    public string name { get; set; }
    public IFormFile? Image { get; set; }
    [Required]
    public Status status { get; set; }
    [Required]
    public string Description { get; set; }
}
