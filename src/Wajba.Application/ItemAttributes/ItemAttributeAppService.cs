global using Wajba.Models.ItemAttributeDomain;
global using Wajba.Dtos.ItemAttributes;

namespace Wajba.ItemAttributes;

[RemoteService(false)]
public class ItemAttributeAppService : CrudAppService<
ItemAttribute,
ItemAttributeDto,
int,
PagedAndSortedResultRequestDto,
CreateItemAttributeDto>,
IItemAttributeAppService
{
    public ItemAttributeAppService(IRepository<ItemAttribute, int> repository)
        : base(repository)
    {
    }
}