global using Wajba.Dtos.OrderSetupContract;
global using Wajba.Models.OrderSetup;

namespace Wajba.OrderSetupService
{
    [RemoteService(false)]
    public class OrderSetupAppService : ApplicationService
    {
        private readonly IRepository<OrderSetup, int> _orderSetupRepository;

        public OrderSetupAppService(IRepository<OrderSetup, int> orderSetupRepository)
        {
            _orderSetupRepository = orderSetupRepository;
        }

        public async Task<OrderSetupDto> CreateAsync(CreateUpdateOrderSetupDto input)
        {

            OrderSetup orderSetup = new OrderSetup()
            {
                BasicDeliveryCharge = input.BasicDeliveryCharge,
                ChargePerKilo = input.ChargePerKilo,
                FoodPreparationTime = input.FoodPreparationTime,
                FreeDeliveryKilometer = input.FreeDeliveryKilometer,
                IsDeliveryEnabled = input.IsDeliveryEnabled,
                IsTakeawayEnabled = input.IsTakeawayEnabled,
                ScheduleOrderSlotDuration = input.ScheduleOrderSlotDuration,
                Ontime = input.Ontime,
                Warning = input.Warning,
                DelayTime = input.DelayTime
            };

            var insertedOrderSetup = await _orderSetupRepository.InsertAsync(orderSetup, true);
            return ObjectMapper.Map<OrderSetup, OrderSetupDto>(insertedOrderSetup);
        }

        public async Task<OrderSetupDto> UpdateAsync(UpdateOrderSetupDto input)
        {
            // Company company = await _repository.FirstOrDefaultAsync();
            OrderSetup orderSetup = await _orderSetupRepository.FirstOrDefaultAsync();
            if (orderSetup == null)
                throw new Exception("Not Found");
            orderSetup.BasicDeliveryCharge = input.BasicDeliveryCharge;
            orderSetup.ChargePerKilo = input.ChargePerKilo;
            orderSetup.FoodPreparationTime = input.FoodPreparationTime;
            orderSetup.FreeDeliveryKilometer = input.FreeDeliveryKilometer;
            orderSetup.IsDeliveryEnabled = input.IsDeliveryEnabled;
            orderSetup.IsTakeawayEnabled = input.IsTakeawayEnabled;
            orderSetup.ScheduleOrderSlotDuration = input.ScheduleOrderSlotDuration;
            orderSetup.LastModificationTime = DateTime.Now;
            orderSetup.Ontime = input.Ontime;
            orderSetup.Warning = input.Warning;
            orderSetup.DelayTime = input.DelayTime;
            //ObjectMapper.Map(input, orderSetup);
            var updatedOrderSetup = await _orderSetupRepository.UpdateAsync(orderSetup, true);
            return ObjectMapper.Map<OrderSetup, OrderSetupDto>(updatedOrderSetup);
        }

        public async Task<OrderSetupDto> GetByIdAsync(int id)
        {
            var orderSetup = await _orderSetupRepository.GetAsync(id);
            if (orderSetup == null)
                throw new EntityNotFoundException(typeof(OrderSetup), id);
            return ObjectMapper.Map<OrderSetup, OrderSetupDto>(orderSetup);
        }

        public async Task<PagedResultDto<OrderSetupDto>> GetListAsync(GetOrderSetupInput input)
        {
            var queryable = await _orderSetupRepository.GetQueryableAsync();
            var totalCount = await AsyncExecuter.CountAsync(queryable);
            var items = await AsyncExecuter.ToListAsync(queryable
                .OrderBy(input.Sorting ?? nameof(OrderSetup.FoodPreparationTime))
                .PageBy(input.SkipCount, input.MaxResultCount));

            return new PagedResultDto<OrderSetupDto>(
                totalCount,
                ObjectMapper.Map<List<OrderSetup>, List<OrderSetupDto>>(items)
            );
        }

        public async Task DeleteAsync(int id)
        {
            OrderSetup orderSetup = await _orderSetupRepository.GetAsync(id);
            if (orderSetup == null)
                throw new EntityNotFoundException(typeof(OrderSetup), id);
            await _orderSetupRepository.DeleteAsync(id);
        }
    }
}