using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Wajba.Dtos.ItemAddonContract;
using Wajba.Models.ItemAddonDomain;

namespace Wajba.ItemAddonService
{
    [RemoteService(false)]
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
            return ObjectMapper.Map<List<ItemAddon>, List<ItemAddonDto>>(itemAddons);
        }

        public async Task<ItemAddonDto> GetByIdAsync(int id)
        {
            var itemAddon = await _itemAddonRepository.GetAsync(id);
            return ObjectMapper.Map<ItemAddon, ItemAddonDto>(itemAddon);
        }

        public async Task<ItemAddonDto> CreateAsync(CreateUpdateItemAddonDto input)
        {
            var itemAddon = ObjectMapper.Map<CreateUpdateItemAddonDto, ItemAddon>(input);
            await _itemAddonRepository.InsertAsync(itemAddon);
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
            await _itemAddonRepository.UpdateAsync(itemAddon);
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
}
