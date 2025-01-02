namespace Wajba.OffersContract
{
    public interface IOfferService 
    {
        Task<OfferDto> GetAsync(int id);
        Task<List<OfferDto>> GetAllAsync();
        Task<OfferDto> CreateAsync(CreateUpdateOfferDto input);
        Task<OfferDto> UpdateAsync(int id, CreateUpdateOfferDto input);
        Task DeleteAsync(int id);
    }
}
