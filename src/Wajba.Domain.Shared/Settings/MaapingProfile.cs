//namespace FosAPI.Settings
//{
//    public class MaapingProfile : Profile
//    {
//        public MaapingProfile()
//        {
//            CreateMap<Category, CategoryDTO>().ForMember(p => p.Id, p => p.MapFrom(p => p.Id))
//                .ForMember(p => p.name, p => p.MapFrom(p => p.name))
//                .ReverseMap();
//            CreateMap<UpdateItemDTO, Item>().ForMember(p => p.Id, p => p.MapFrom(p => p.Id))
//                .ForMember(p => p.Name, p => p.MapFrom(p => p.Name))
//                .ForMember(p => p.Price, p => p.MapFrom(p => p.Price))
//                .ForMember(p => p.Description, p => p.MapFrom(p => p.Description))
//                .ForMember(p => p.ImageUrl, p => p.MapFrom(p => p.ImageUrl))
//                .ReverseMap();

//        }
//    }
//}
