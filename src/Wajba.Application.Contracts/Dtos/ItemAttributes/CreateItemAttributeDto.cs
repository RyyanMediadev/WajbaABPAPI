namespace Wajba.Dtos.ItemAttributes;

public class CreateItemAttributeDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int Status { get; set; }
}
