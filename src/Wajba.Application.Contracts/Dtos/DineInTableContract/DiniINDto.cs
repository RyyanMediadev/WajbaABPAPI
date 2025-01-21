namespace Wajba.Dtos.DineInTableContract;

public class DiniINDto:EntityDto<int>
{
    public string Name { get; set; }
    public byte Size { get; set; }
    public int Status { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string url { get; set; }
}
