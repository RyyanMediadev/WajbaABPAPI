global using Wajba.Models.ItemAttributeDomain;
global using Wajba.Dtos.ItemAttributes;

namespace Wajba.ItemAttributes;

[RemoteService(false)]
public class ItemAttributeAppService : ApplicationService, IItemAttributeAppService
{
    private readonly IRepository<ItemAttribute,int> _itemAttributeRepository;

    public ItemAttributeAppService(IRepository<ItemAttribute, int> itemAttributeRepository)
    {
        _itemAttributeRepository = itemAttributeRepository;
    }

    public async Task<PagedResultDto<ItemAttributeDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _itemAttributeRepository.GetQueryableAsync();
        var totalCount = await AsyncExecuter.CountAsync(query);
        var items = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

        return new PagedResultDto<ItemAttributeDto>(totalCount, ObjectMapper.Map<List<ItemAttribute>, List<ItemAttributeDto>>(items));
    }

    public async Task<ItemAttributeDto> GetAsync(int id)
    {
        var entity = await _itemAttributeRepository.GetAsync(id);
        return ObjectMapper.Map<ItemAttribute, ItemAttributeDto>(entity);
    }

    public async Task<ItemAttributeDto> CreateAsync(CreateItemAttributeDto input)
    {
        var entity = new ItemAttribute
        {
           Name=input.Name,
           Status=input.Status
        };
       var insertedattr= await _itemAttributeRepository.InsertAsync(entity);
        return ObjectMapper.Map<ItemAttribute, ItemAttributeDto>(insertedattr);
    }

    public async Task<ItemAttributeDto> UpdateAsync(int id, UpdateItemAttributeDto input)
    {
        var entity = await _itemAttributeRepository.GetAsync(id);
        ObjectMapper.Map(input, entity);
        await _itemAttributeRepository.UpdateAsync(entity);
        return ObjectMapper.Map<ItemAttribute, ItemAttributeDto>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _itemAttributeRepository.DeleteAsync(id);
    }
}