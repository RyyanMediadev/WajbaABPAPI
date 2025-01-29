
using Wajba.AddSpecification;
using Wajba.Models.BranchDomain;

namespace Fos_EF.AddSpecification;

public class DineTableSpecification : BaseSpecification<DineInTable>
{
    public DineTableSpecification(
    string? name = null,
    int? size = null,
    bool? status = null,
    int? branchId = null,
    int pageNumber = 1,
    int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(name))
        {
            AddCriteria(d => d.Name == name);
        }
        if (size.HasValue)
        {
            AddCriteria(d => d.Size == size.Value);
        }
        if (status.HasValue)
        {
            //AddCriteria(d => d.Status == status.HasValue);
        }
        if (branchId.HasValue)
        {
            AddCriteria(d => d.BranchId == branchId.Value);
        }

        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
    }
}
