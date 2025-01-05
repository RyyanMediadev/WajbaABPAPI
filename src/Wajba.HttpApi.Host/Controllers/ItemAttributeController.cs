global using Wajba.Dtos.ItemAttributes;

namespace Wajba.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemAttributeController : AbpController
{
    private readonly IItemAttributeAppService _itemAttributeAppService;

    public ItemAttributeController(IItemAttributeAppService itemAttributeAppService)
    {
        _itemAttributeAppService = itemAttributeAppService;
    }

    [HttpGet]
    public async Task<PagedResultDto<ItemAttributeDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
    {
        return await _itemAttributeAppService.GetListAsync(input);
    }

    [HttpGet("{id}")]
    public async Task<ItemAttributeDto> GetAsync(int id)
    {
        return await _itemAttributeAppService.GetAsync(id);
    }

    [HttpPost]
    public async Task<ItemAttributeDto> CreateAsync(CreateUpdateItemAttributeDto input)
    {
        return await _itemAttributeAppService.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public async Task<ItemAttributeDto> UpdateAsync(int id, CreateUpdateItemAttributeDto input)
    {
        return await _itemAttributeAppService.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(int id)
    {
        await _itemAttributeAppService.DeleteAsync(id);
    }
}