using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Wajba.Dtos.ItemExtraContract;
using Wajba.Models.ItemExtraDomain;
using Wajba.Models.Items;

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

        public async Task<ItemExtraDto> GetAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
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

     
        
        public async Task<ItemExtraDto> UpdateForSpecificItemAsync(int itemId, int extraId, UpdateItemExtraDto input)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.ItemId == itemId && x.Id == extraId);
            if (entity == null)
            {
                throw new EntityNotFoundException($"Extra with ID {extraId} for Item {itemId} not found.");
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
