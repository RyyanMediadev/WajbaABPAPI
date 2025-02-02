﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.ItemVariationContract
{
    public interface IItemVariationAppService : IApplicationService
    {
        Task<ItemVariationDto> GetAsync(int itemid, int variationid);

        Task<List<ItemVariationDto>> GetListByItemAttributeIdAsync(int itemAttributeId);
        Task<List<ItemVariationDto>> GetListByItemIdAsync(int itemId);
        Task<ItemVariationDto> CreateAsync(CreateItemVariationDto input);
        Task<ItemVariationDto> UpdateForSpecificItemAsync( UpdateItemVariationDto input);
        Task DeleteForSpecificItemAsync(int itemId, int variationId);
    }
}
