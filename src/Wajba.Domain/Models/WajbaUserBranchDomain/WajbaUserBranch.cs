


using Wajba.Models.WajbaUserDomain;

public class WajbaUserBranch : FullAuditedEntity<int>
    {
        public int WajbaUserId { get; set; }
        public WajbaUser WajbaUser { get; set; }
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
    }

