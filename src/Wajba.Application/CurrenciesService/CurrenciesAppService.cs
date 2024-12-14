﻿global using Wajba.CurrenciesContract;
global using Wajba.Models.CurrenciesDomain;
using Wajba.Dtos.CurrenciesContract;

namespace Wajba.CurrenciesService
{
    [RemoteService(false)]
    public class CurrenciesAppService : CrudAppService<
    Currencies,
    CurrenciesDto,
    int,
    PagedAndSortedResultRequestDto,
    CreateUpdateCurrenciesDto>,
    ICurrenciesAppService
    {
        public CurrenciesAppService(IRepository<Currencies, int> repository)
            : base(repository)
        {
        }
    }
}
