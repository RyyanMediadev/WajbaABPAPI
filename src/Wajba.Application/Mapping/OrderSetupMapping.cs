namespace Wajba.Mapping;

public class OrderSetupMapping:Profile
{
    public OrderSetupMapping()
    {

        CreateMap<OrderSetup, OrderSetupDto>()
            .ForMember(OrderSetupDto => OrderSetupDto.Id, opt => opt.MapFrom(OrderSetup => OrderSetup.Id))
            .ForMember(OrderSetupDto => OrderSetupDto.ScheduleOrderSlotDuration, opt => opt.MapFrom(OrderSetup => OrderSetup.ScheduleOrderSlotDuration))
            .ForMember(OrderSetupDto => OrderSetupDto.BasicDeliveryCharge, opt => opt.MapFrom(OrderSetup => OrderSetup.BasicDeliveryCharge))
            .ForMember(OrderSetupDto => OrderSetupDto.ChargePerKilo, opt => opt.MapFrom(OrderSetup => OrderSetup.ChargePerKilo))
            .ForMember(OrderSetupDto => OrderSetupDto.IsTakeawayEnabled, opt => opt.MapFrom(OrderSetup => OrderSetup.IsTakeawayEnabled))
            .ForMember(OrderSetupDto => OrderSetupDto.FoodPreparationTime, opt => opt.MapFrom(OrderSetup => OrderSetup.FoodPreparationTime))
            .ForMember(OrderSetupDto => OrderSetupDto.IsDeliveryEnabled, opt => opt.MapFrom(OrderSetup => OrderSetup.IsDeliveryEnabled))
            .ForMember(OrderSetupDto => OrderSetupDto.FreeDeliveryKilometer, opt => opt.MapFrom(OrderSetup => OrderSetup.FreeDeliveryKilometer))
            .ReverseMap();

    }
}
