global using Wajba.Dtos.ThemesContract;

namespace Wajba.Dtos.PopularItemstoday;

public class CreatePopularitem
{
    public int ItemId { get; set; }
    public Base64ImageModel Model { get; set; }
    public decimal preprice { get; set; }
    public decimal currentprice { get; set; }
    public string Description { get; set; }
}
