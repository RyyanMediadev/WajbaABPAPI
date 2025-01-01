using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.ItemAddonContract
{
    public interface IItemAddonAppService : IApplicationService
    {
        Task<List<ItemAddonDto>> GetByItemIdAsync(int itemId);
        Task<ItemAddonDto> GetByIdAsync(int id);
        Task<ItemAddonDto> CreateAsync(CreateUpdateItemAddonDto input);
        Task<ItemAddonDto> UpdateForSpecificItemAsync(int itemId, int addonId, CreateUpdateItemAddonDto input);
        Task DeleteForSpecificItemAsync(int itemId, int addonId);
    }
}
