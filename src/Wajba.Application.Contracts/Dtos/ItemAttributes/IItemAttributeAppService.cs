namespace Wajba.Dtos.ItemAttributes;

public interface IItemAttributeAppService :
ICrudAppService<
    ItemAttributeDto,
    int,
    PagedAndSortedResultRequestDto,
    CreateItemAttributeDto>
{
}
