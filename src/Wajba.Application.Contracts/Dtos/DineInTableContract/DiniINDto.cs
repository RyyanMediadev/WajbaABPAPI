namespace Wajba.Dtos.DineInTableContract;

public class DiniINDto:EntityDto<int>
{
    public string Name { get; set; }
    public byte Size { get; set; }
    public int Status { get; set; }
    public int BranchId { get; set; }
}
