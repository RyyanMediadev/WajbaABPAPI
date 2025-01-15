namespace Wajba.Mapping;

public class DineTableMapping:Profile
{
    public DineTableMapping()
    {
        CreateMap<DineInTable, DiniINDto>()
            .ForMember(p => p.Name, p => p.MapFrom(p => p.Name))
            .ForMember(p => p.BranchId, p => p.MapFrom(p => p.BranchId))
            .ForMember(p => p.IsActive, p => p.MapFrom(p => p.Status))
            .ForMember(p => p.Size, p => p.MapFrom(p => p.Size))
            .ReverseMap();
        CreateMap<DineInTable, CreateDineIntable>()
            .ForMember(p => p.Name, p => p.MapFrom(p => p.Name))
            .ForMember(p => p.status, p => p.MapFrom(p => p.Status))
            .ForMember(p => p.BranchId, p => p.MapFrom(p => p.BranchId))
            .ForMember(p => p.Size, p => p.MapFrom(p => p.Size)).ReverseMap();
    }
}
