global using Wajba.Models.ItemTaxDomain;
global using Wajba.Dtos.ItemTaxContract;

namespace Wajba.ItemTaxService;

[RemoteService(false)]
public class ItemTaxAppService : ApplicationService
{
    private readonly IRepository<ItemTax, int> _repository;

    public ItemTaxAppService(IRepository<ItemTax, int> repository)
    {
       _repository = repository;
    }
    public async Task<ItemTaxDto> CreateAsync(CreateItemTaxDto input)
    {
        ItemTax itemTax = new ItemTax
        {
            Code = input.Code,
            Name = input.Name,
            Status = input.Status,
            TaxRate = input.TaxRate
        };
        var insertedtax=await _repository.InsertAsync(itemTax, true);
        return ObjectMapper.Map<ItemTax, ItemTaxDto>(insertedtax);
    }
    public async Task<ItemTaxDto> UpdateAsync(int id, UpdateItemTaxDto input)
    {
        ItemTax itemTax = await _repository.GetAsync(id);
        if (itemTax == null)
            throw new EntityNotFoundException(typeof(ItemTax), id);
        itemTax.Code = input.Code;
        itemTax.Name = input.Name;
        itemTax.Status = input.Status;
        itemTax.TaxRate = input.TaxRate;
        itemTax.LastModificationTime = DateTime.UtcNow;
        ItemTax updatedItemTax = await _repository.UpdateAsync(itemTax, true);
        return ObjectMapper.Map<ItemTax, ItemTaxDto>(updatedItemTax);
    }
    public async Task<ItemTaxDto> GetByIdAsync(int id)
    {
        ItemTax itemTax = await _repository.GetAsync(id);
        if (itemTax == null)
            throw new EntityNotFoundException(typeof(ItemTax), id);
        return ObjectMapper.Map<ItemTax, ItemTaxDto>(itemTax);
    }
    public async Task<PagedResultDto<ItemTaxDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        var itemTaxes = await _repository.GetListAsync();
        return new PagedResultDto<ItemTaxDto>
        {
            TotalCount = itemTaxes.Count,
            Items = ObjectMapper.Map<List<ItemTax>, List<ItemTaxDto>>(itemTaxes)
        };
    }
    public async Task DeleteAsync(int id)
    {
        ItemTax itemTax = await _repository.GetAsync(id);
        if (itemTax == null)
            throw new EntityNotFoundException(typeof(ItemTax), id);
        await _repository.DeleteAsync(itemTax);
    }
}