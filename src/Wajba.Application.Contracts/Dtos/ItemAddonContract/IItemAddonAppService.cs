using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.ItemAddonContract
{
    public interface IItemAddonAppService
    {
        Task<List<ItemAddonDto>> GetByItemIdAsync(int itemId);
        Task<ItemAddonDto> GetByIdAsync(int itemid,int addonid);
        Task<ItemAddonDto> CreateAsync(CreateItemAddonDto input);
        Task<ItemAddonDto> UpdateForSpecificItemAsync(int itemId, int addonId, UpdateItemAddonDto input);
        Task DeleteForSpecificItemAsync(int itemId, int addonId);
    }
}
