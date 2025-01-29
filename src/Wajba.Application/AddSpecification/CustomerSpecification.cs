
using Wajba.AddSpecification;

namespace Fos_EF.AddSpecification;

public class CustomerSpecification : BaseSpecification<WajbaUser>
{
    public CustomerSpecification(
       
         string? name,
         string? email,
         string? phone,
        Status? status,
        int? pageNumber,
         int? pageSize)
    {
        
        if (!string.IsNullOrEmpty(name))
            AddCriteria(p => p.FullName.ToLower() == name.ToLower());

        if (!string.IsNullOrEmpty(email))
            AddCriteria(p => p.Email == email);
        if (!string.IsNullOrEmpty(phone))
            AddCriteria(p => p.Phone == phone);
        if (status.HasValue)
        {
            AddCriteria(e => e.status == status);
        }
        if (pageNumber.HasValue)
            ApplyPaging((pageNumber.Value - 1) * pageSize.Value, pageSize.Value);
    }
}