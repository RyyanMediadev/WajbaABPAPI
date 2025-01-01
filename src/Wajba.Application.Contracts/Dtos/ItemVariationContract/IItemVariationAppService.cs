using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.ItemVariationContract
{
    public interface IItemVariationAppService : IApplicationService
    {
        Task<ItemVariationDto> GetAsync(int id);
        Task<List<ItemVariationDto>> GetListByItemIdAsync(int itemId);
        Task<ItemVariationDto> CreateAsync(CreateItemVariationDto input);
        Task<ItemVariationDto> UpdateForSpecificItemAsync(int itemId, int variationId, UpdateItemVariationDto input);
        Task DeleteForSpecificItemAsync(int itemId, int variationId);
    }
}
