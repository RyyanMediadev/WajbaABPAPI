namespace Wajba.Dtos.DineInTableContract;

public class CreateDineIntable
{
    [Required]
    public string Name { get; set; }
    [Required]
    public byte Size { get; set; }
    [Required]
    public Status status { get; set; }
    [Required]
    public int BranchId { get; set; }
}
