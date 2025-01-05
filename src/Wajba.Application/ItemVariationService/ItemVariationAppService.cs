using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Wajba.Dtos.ItemVariationContract;
using Wajba.Models.ItemVariationDomain;

namespace Wajba.ItemVariationService
{
    [RemoteService(false)]
    public class ItemVariationAppService : ApplicationService, IItemVariationAppService
    {
        private readonly IRepository<ItemVariation, int> _repository;

        public ItemVariationAppService(IRepository<ItemVariation, int> repository)
        {
            _repository = repository;
        }

        public async Task<ItemVariationDto> GetAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            return ObjectMapper.Map<ItemVariation, ItemVariationDto>(entity);
        }

        public async Task<List<ItemVariationDto>> GetListByItemIdAsync(int itemId)
        {
            var entities = await _repository.GetListAsync(x => x.ItemId == itemId);
            return ObjectMapper.Map<List<ItemVariation>, List<ItemVariationDto>>(entities);
        }

        public async Task<ItemVariationDto> CreateAsync(CreateItemVariationDto input)
        {
            var entity = ObjectMapper.Map<CreateItemVariationDto, ItemVariation>(input);
            await _repository.InsertAsync(entity);
            return ObjectMapper.Map<ItemVariation, ItemVariationDto>(entity);
        }

        public async Task<ItemVariationDto> UpdateForSpecificItemAsync(int itemId, int variationId, UpdateItemVariationDto input)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.ItemId == itemId && x.Id == variationId);
            if (entity == null)
            {
                throw new EntityNotFoundException($"Variation with ID {variationId} for Item {itemId} not found.");
            }

            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity);
            return ObjectMapper.Map<ItemVariation, ItemVariationDto>(entity);
        }

        public async Task DeleteForSpecificItemAsync(int itemId, int variationId)
        {
            var itemVariation = await _repository.FirstOrDefaultAsync(x => x.ItemId == itemId && x.Id == variationId);
            if (itemVariation == null)
            {
                throw new EntityNotFoundException($"Variation with ID {variationId} for Item {itemId} not found.");
            }

            await _repository.DeleteAsync(itemVariation);
        }
    }
}
