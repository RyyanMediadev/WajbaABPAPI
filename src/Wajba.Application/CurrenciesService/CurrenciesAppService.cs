global using Wajba.Dtos.CurrenciesContract;
global using Wajba.Models.CurrenciesDomain;

namespace Wajba.CurrenciesService
{
    [RemoteService(false)]
    public class CurrenciesAppService : ApplicationService
    {
        private readonly IRepository<Currencies, int> _repository;

        public CurrenciesAppService(IRepository<Currencies, int> repository)
        {
            _repository = repository;
        }
        public async Task<CurrenciesDto> CreateAsync(CreateUpdateCurrenciesDto input)
        {
            Currencies currencies = new Currencies
            {
                Name = input.Name,
                Code = input.Code,
                Symbol = input.Symbol,
                ExchangeRate = input.ExchangeRate
            };
            var insertedCurrencies = await _repository.InsertAsync(currencies,true);
            return ObjectMapper.Map<Currencies, CurrenciesDto>(insertedCurrencies);
        }
        public async Task<CurrenciesDto> UpdateAsync(int id, UpadteCurrency input)
        {
            Currencies currencies = await _repository.GetAsync(id);
            if (input == null)
                throw new Exception("Not found");
            currencies.Name = input.Name;
            currencies.Code = input.Code;
            currencies.Symbol = input.Symbol;
            currencies.ExchangeRate = input.ExchangeRate;
            currencies.LastModificationTime = DateTime.UtcNow;
            Currencies updatedCurrencies = await _repository.UpdateAsync(currencies,true);
            return ObjectMapper.Map<Currencies, CurrenciesDto>(updatedCurrencies);
        }
        public async Task<CurrenciesDto> GetByIdAsync(int id)
        {
            Currencies currencies = await _repository.GetAsync(id);
            if (currencies == null)
                throw new Exception("Not found");
            return ObjectMapper.Map<Currencies, CurrenciesDto>(currencies);
        }
        public async Task<PagedResultDto<CurrenciesDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
        {
            var currencies = await _repository.GetListAsync();
            return new PagedResultDto<CurrenciesDto>
            {
                TotalCount = currencies.Count,
                Items = ObjectMapper.Map<List<Currencies>, List<CurrenciesDto>>(currencies)
            };
        }
        public async Task DeleteAsync(int id)
        {
         Currencies currencies = await _repository.GetAsync(id);
            if (currencies == null)
                throw new Exception("Not found");
            await _repository.DeleteAsync(id,true);
        }
    }
}
