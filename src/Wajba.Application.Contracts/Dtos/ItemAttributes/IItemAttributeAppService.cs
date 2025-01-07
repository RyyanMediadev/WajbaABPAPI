namespace Wajba.Dtos.ItemAttributes;

public interface IItemAttributeAppService : IApplicationService
{
    Task<PagedResultDto<ItemAttributeDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<ItemAttributeDto> GetAsync(int id);
    Task<ItemAttributeDto> CreateAsync(CreateItemAttributeDto input);
    Task<ItemAttributeDto> UpdateAsync(int id, UpdateItemAttributeDto input);
    Task DeleteAsync(int id);
}
