global using Wajba.Models.PopularItemsDomain;
using Wajba.Dtos.PopularItemstoday;

namespace Wajba.PopularItemServices;
[RemoteService(false)]
public class PopularItemAppservice:ApplicationService
{
    private readonly IRepository<PopularItem, int> _popularitemrepo;

    public PopularItemAppservice(IRepository<PopularItem ,int> popularitemrepo)
    {
       _popularitemrepo = popularitemrepo;
    }
    public async Task<List<Popularitemdto>> GetPopularItems(GetPopulariteminput input)
    {
        var popularitems = await _popularitemrepo.GetListAsync();
        return ObjectMapper.Map<List<PopularItem>, List<Popularitemdto>>(popularitems);
    }
    public async Task<Popularitemdto> GetPopularItemById(int id)
    {
        var popularitem = await _popularitemrepo.GetAsync(id);
        if (popularitem == null)
        {
            throw new EntityNotFoundException(typeof(PopularItem), id);
        }
        return ObjectMapper.Map<PopularItem, Popularitemdto>(popularitem);
    }
}
