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
