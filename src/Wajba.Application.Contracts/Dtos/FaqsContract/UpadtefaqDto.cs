namespace Wajba.Dtos.FaqsContract;

public class UpadtefaqDto
{
    public int Id { get; set; }
    [Required]
    public string Question { get; set; }
    [Required]
    public string Answer { get; set; }
}