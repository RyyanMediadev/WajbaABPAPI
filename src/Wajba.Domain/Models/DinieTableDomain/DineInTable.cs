namespace Wajba.Models.BranchDomain;

public class DineInTable : FullAuditedEntity<int>
{
    public string Name { get; set; } // Table name
    public int Size { get; set; } // Optional size
    public string QrCode { get; set; } // QR code (optional)
    [ForeignKey("Branch")]
    public int BranchId { get; set; } // Foreign key to Branch
    public virtual Branch Branch { get; set; }
    public bool Status { get; set; }

    public DineInTable()
    {

    }
}
