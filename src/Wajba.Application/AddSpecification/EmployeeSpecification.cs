//using Wajba.AddSpecification;

//namespace Fos_EF.AddSpecification;

//public class EmployeeSpecification : BaseSpecification<WajbaUser>
//{
//    public EmployeeSpecification(int pageNumber, int pageSize, string? name, string? Email, string? phone, int? RoleId, Status? status, int? userType)
//    {
//        if (!string.IsNullOrEmpty(name))
//        {
//            AddCriteria(c => c.FullName == name);
//        }
//        if (!string.IsNullOrEmpty(Email))
//        {
//            AddCriteria(c => c.Email == name);
//        }
//        if (!string.IsNullOrEmpty(phone))
//        {
//            AddCriteria(c => c.Phone == phone);
//        }
//        if (RoleId.HasValue)
//        {
//            AddCriteria(c => c.UserRoles.Any(ur => ur.RoleId == RoleId.Value));
//        }
//        if (status.HasValue)
//        {
//            AddCriteria(e => e.status == status);
//        }
//        if (userType.HasValue)
//        {
//            AddCriteria(u => (int)u.UserType == userType.Value);
//        }
//        // Include the UserRoles and Role
//        AddInclude(c => c.UserRoles);
//        AddInclude("UserRoles.Role");

//        // Include the UserBranches and Branch
//        AddInclude(c => c.UserBranches);
//        AddInclude("UserBranches.Branch");
//        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
//    }
//}