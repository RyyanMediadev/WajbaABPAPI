namespace Wajba.Mapping
{
    public class CartItemMappingProfile : Profile
    {
        public CartItemMappingProfile()
        {
            CreateMap<CartItem, CartItemDto>();
            CreateMap<CartItemDto, CartItem>();

        }
    }
}
