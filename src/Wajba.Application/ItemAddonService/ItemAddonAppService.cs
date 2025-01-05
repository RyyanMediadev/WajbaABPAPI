namespace Wajba.ItemAddonService;

public class ItemAddonAppService : ApplicationService, IItemAddonAppService
{
    private readonly IRepository<ItemAddon, int> _itemAddonRepository;

    public ItemAddonAppService(IRepository<ItemAddon, int> itemAddonRepository)
    {
        _itemAddonRepository = itemAddonRepository;
    }

    public async Task<List<ItemAddonDto>> GetByItemIdAsync(int itemId)
    {
        var itemAddons = await _itemAddonRepository.GetListAsync(x => x.ItemId == itemId);
        if (itemAddons == null)
        {
            throw new EntityNotFoundException(typeof(ItemAddon), itemId);
        }
        return ObjectMapper.Map<List<ItemAddon>, List<ItemAddonDto>>(itemAddons);
    }

    public async Task<ItemAddonDto> GetByIdAsync(int id)
    {
        var itemAddon = await _itemAddonRepository.GetAsync(id);
        if (itemAddon == null)
            throw new EntityNotFoundException(typeof(ItemAddon), id);
        return ObjectMapper.Map<ItemAddon, ItemAddonDto>(itemAddon);
    }

    public async Task<ItemAddonDto> CreateAsync(CreateUpdateItemAddonDto input)
    {
        var itemAddon = ObjectMapper.Map<CreateUpdateItemAddonDto, ItemAddon>(input);
        await _itemAddonRepository.InsertAsync(itemAddon, true);
        return ObjectMapper.Map<ItemAddon, ItemAddonDto>(itemAddon);
    }

    public async Task<ItemAddonDto> UpdateForSpecificItemAsync(int itemId, int addonId, CreateUpdateItemAddonDto input)
    {
        var itemAddon = await _itemAddonRepository.FirstOrDefaultAsync(x => x.ItemId == itemId && x.Id == addonId);
        if (itemAddon == null)
        {
            throw new EntityNotFoundException($"Addon with ID {addonId} for Item {itemId} not found.");
        }
        ObjectMapper.Map(input, itemAddon);
        await _itemAddonRepository.UpdateAsync(itemAddon, true);
        return ObjectMapper.Map<ItemAddon, ItemAddonDto>(itemAddon);
    }

    public async Task DeleteForSpecificItemAsync(int itemId, int addonId)
    {
        var itemAddon = await _itemAddonRepository.FirstOrDefaultAsync(x => x.ItemId == itemId && x.Id == addonId);
        if (itemAddon == null)
        {
            throw new EntityNotFoundException($"Addon with ID {addonId} for Item {itemId} not found.");
        }
        await _itemAddonRepository.DeleteAsync(itemAddon);
    }
}
