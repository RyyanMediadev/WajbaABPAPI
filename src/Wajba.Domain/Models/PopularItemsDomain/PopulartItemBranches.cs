namespace Wajba.Models.PopularItemsDomain;

public class PopulartItemBranches: FullAuditedEntity<int>
{
    public int BranchId {  get; set; }
    public Branch Branch { get; set; }
    public int PopularItemId {  get; set; }
    public PopularItem PopularItem { get; set; }
}
