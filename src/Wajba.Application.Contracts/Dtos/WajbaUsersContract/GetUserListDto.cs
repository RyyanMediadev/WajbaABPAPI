namespace Wajba.Dtos.WajbaUsersContract;

public class GetUserListDto
{
    public string? FullName { get; set; }
    public int? Type { get; set; }
    public int? Status { get; set; }
    public int MaxResultCount { get; set; } = 10;
    public int SkipCount { get; set; } = 0;
}
