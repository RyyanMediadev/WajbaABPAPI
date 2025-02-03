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
            throw new Exception("It is Found");
        UserRole userRole = new UserRole() { RoleName = createRole.Name };
        UserRole user = await _rolerepo.InsertAsync(userRole, true);
        return toRolesDto(user);
    }
    public async Task<RolesDto> Update(UpdateRole updateRole)
    {
        UserRole role = await _rolerepo.FirstOrDefaultAsync(p => p.Id == updateRole.Id);
        if (role == null) throw new EntityNotFoundException("Not Found");
        role.RoleName = updateRole.Name;
        role.LastModificationTime = DateTime.Now;
        await _rolerepo.UpdateAsync(role);
        return toRolesDto(role);
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
    public async Task<RolesDto> GetById(int id)
    {
        UserRole role = await _rolerepo.FirstOrDefaultAsync(p => p.Id == id);
        if (role == null)
            throw new EntityNotFoundException("Not Found");
        return toRolesDto(role);
    }
    public async Task DeleteAsync(int id)
    {
        UserRole role = await _rolerepo.FirstOrDefaultAsync(p => p.Id == id);
        if (role == null)
            throw new EntityNotFoundException("Not Found");
        await _rolerepo.HardDeleteAsync(role, true);
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