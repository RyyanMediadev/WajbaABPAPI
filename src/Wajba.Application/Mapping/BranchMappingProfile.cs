using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Mapping
{
    public class BranchMappingProfile : Profile
    {
        public BranchMappingProfile()
        {
            CreateMap<Branch, BranchDto>();
            CreateMap<CreateBranchDto, Branch>();
        }
    }
}
