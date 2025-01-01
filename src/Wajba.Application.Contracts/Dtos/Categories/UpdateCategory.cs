namespace Wajba.Dtos.Categories;

public class UpdateCategory
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string name { get; set; }
    [Required]
    public IFormFile? Image { get; set; }
    [Required]
    public Status status { get; set; }
    [Required]
    public string Description { get; set; }
}
