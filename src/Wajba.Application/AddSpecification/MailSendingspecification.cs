
//namespace Fos_EF.AddSpecification;

//public class MailSendingspecification : BaseSpecification<SendingEmail>
//{
//    public MailSendingspecification(
//   string? from,
//   string? to,
//   int? branchid,
//    DateTime? createdAtStart,
//    DateTime? createdAtEnd,
//    //bool? isDeleted,
//    int? pageNumber,
//    int? pageSize)
//    {
//        if (branchid.HasValue)
//            AddCriteria(p => p.BranchId == branchid.Value);
//        if (!string.IsNullOrWhiteSpace(from))
//            AddCriteria(p => p.From.ToLower() == from.ToLower());
//        if (!string.IsNullOrWhiteSpace(to))
//            AddCriteria(p => p.To.ToLower() == to.ToLower());
//        if (createdAtStart.HasValue)
//            AddCriteria(p => p.CreatedAt >= createdAtStart.Value);
//        if (createdAtEnd.HasValue)
//            AddCriteria(p => p.CreatedAt <= createdAtEnd.Value);
//        //if (isDeleted.HasValue)
//        //    AddCriteria(p => p.IsDeleted == isDeleted.Value);
//        if (pageNumber.HasValue)
//            ApplyPaging((pageNumber.Value - 1) * pageSize.Value, pageSize.Value);
//    }
//}