global using Wajba.Enums;

namespace Wajba.Dtos.Categories;

public class CategoryDto : EntityDto<int>
{
    public string? name { get; set; }
    public string? ImageUrl { get; set; }
    public int status { get; set; }
    public string Description { get; set; }
}
