global using Wajba.Dtos.ItemsDtos;

namespace Wajba.Dtos.Categories;

public class CategoryItemsDto: EntityDto<int>
{
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public string Description { get; set; }
    public int TotalItems { get; set; }
    public Status Status { get; set; }
    public bool IsFilled { get; set; }
    //public List<ItemDto> Items { get; set; } = new List<ItemDto>();
}
