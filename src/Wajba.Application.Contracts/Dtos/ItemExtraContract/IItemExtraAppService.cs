﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wajba.Dtos.ItemExtraContract
{
    public interface IItemExtraAppService : IApplicationService
    {
        Task<ItemExtraDto> GetAsync(int itemid, int extraid);
        Task<List<ItemExtraDto>> GetListByItemIdAsync(int itemId);
        Task<ItemExtraDto> CreateAsync(CreateItemExtraDto input);
        Task<ItemExtraDto> UpdateForSpecificItemAsync(UpdateItemExtraDto input);
        Task DeleteAsync(int itemId, int extraId);
    }
}
