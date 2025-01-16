global using Microsoft.AspNetCore.Http;

namespace Wajba.Dtos.Categories;

public class CreateUpdateCategoryDto
{
    [Required]
    public string name { get; set; }
    public Base64ImageModel? Model { get; set; }
    [Required]
    public int status { get; set; }
    [Required]
    public string Description { get; set; }
}
