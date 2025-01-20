global using Wajba.Dtos.ItemExtraContract;
global using Wajba.Models.ItemExtraDomain;

namespace Wajba.ItemExtraService
{
    [RemoteService(false)]
    public class ItemExtraAppService : ApplicationService, IItemExtraAppService
    {
        private readonly IRepository<ItemExtra, int> _repository;

        public ItemExtraAppService(IRepository<ItemExtra, int> repository)
        {
            _repository = repository;
        }

        public async Task<ItemExtraDto> GetAsync(int itemid,int extraid)
        {
            var entity = await _repository.FirstOrDefaultAsync(
        itemExtra => itemExtra.ItemId == itemid && itemExtra.Id == extraid);

            if (entity == null)
                throw new EntityNotFoundException(typeof(ItemExtra), new { itemid, extraid });
            return ObjectMapper.Map<ItemExtra, ItemExtraDto>(entity);
        }

        public async Task<List<ItemExtraDto>> GetListByItemIdAsync(int itemId)
        {
            var entities = await _repository.GetListAsync(x => x.ItemId == itemId);
            return ObjectMapper.Map<List<ItemExtra>, List<ItemExtraDto>>(entities);
        }

        public async Task<ItemExtraDto> CreateAsync(CreateItemExtraDto input)
        {
            var entity = ObjectMapper.Map<CreateItemExtraDto, ItemExtra>(input);
            await _repository.InsertAsync(entity);
            return ObjectMapper.Map<ItemExtra, ItemExtraDto>(entity);
        }

     
        
        public async Task<ItemExtraDto> UpdateForSpecificItemAsync( UpdateItemExtraDto input)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.ItemId == input.ItemId && x.Id == input.extraId);
            if (entity == null)
            {
                throw new EntityNotFoundException($"Extra with ID {input.extraId} for Item {input.ItemId} not found.");
            }

            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity);
            return ObjectMapper.Map<ItemExtra, ItemExtraDto>(entity);
        }
        public async Task DeleteAsync(int itemId, int extraId)
        {
          

            var itemExtra = await _repository.FirstOrDefaultAsync(x => x.ItemId == itemId && x.Id == extraId);
            if (itemExtra == null)
            {
                throw new EntityNotFoundException($"Extra with ID {extraId} for Item {itemId} not found.");
            }

            await _repository.DeleteAsync(itemExtra);
        }
    }
}
