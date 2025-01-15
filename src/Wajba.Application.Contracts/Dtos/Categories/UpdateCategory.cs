namespace Wajba.Dtos.Categories;

public class UpdateCategory: CreateUpdateCategoryDto
{
    [Required]
    public int Id { get; set; }
}
