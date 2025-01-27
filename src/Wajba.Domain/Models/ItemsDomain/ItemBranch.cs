global using Volo.Abp.Domain.Entities;

namespace Wajba.Models.Items;

public class ItemBranch:FullAuditedEntity<int>
{
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public int BranchId { get; set; }
    public Branch Branch { get; set; }
    public ItemBranch()
    {

    }
}
