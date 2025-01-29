using Wajba.Dtos.RoleContract;
using Wajba.Models.WajbaUserRoleDomain;

namespace Wajba.RolesServices;

public class RoleAppservices : ApplicationService
{
    private readonly IRepository<UserRole, int> _rolerepo;

    public RoleAppservices(IRepository<UserRole, int> rolerepo)
    {
        _rolerepo = rolerepo;
    }
    public async Task<RolesDto> CreateAsync(CreateRole createRole)
    {
        if (await _rolerepo.FirstOrDefaultAsync(p => p.RoleName.ToLower() == createRole.Name.ToLower()) != null)
            throw new Exception("It ism Found");
        UserRole userRole = new UserRole() { RoleName = createRole.Name };
        UserRole user = await _rolerepo.InsertAsync(userRole, true);
        return toRolesDto(user);
    }
    public async Task<PagedResultDto<RolesDto>> GetAll()
    {
        var roles = await _rolerepo.ToListAsync();
        List<RolesDto> rolesDtos = new List<RolesDto>();
        foreach (var role in roles)
        {
            rolesDtos.Add(toRolesDto(role));
        }
        return new PagedResultDto<RolesDto>()
        {
            TotalCount = rolesDtos.Count,
            Items = rolesDtos
        };
    }

    private static RolesDto toRolesDto(UserRole user)
    {
        return new RolesDto
        {
            Id = user.Id,
            Name = user.RoleName,
        };
    }
}