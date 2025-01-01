using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.Dtos.OrderSetupContract;
using Wajba.Models.OrderSetup;

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
            OrderSetup orderSetup=new OrderSetup()
            {
                BasicDeliveryCharge = input.BasicDeliveryCharge,
                ChargePerKilo = input.ChargePerKilo,
                FoodPreparationTime = input.FoodPreparationTime,
                FreeDeliveryKilometer = input.FreeDeliveryKilometer,
                IsDeliveryEnabled = input.IsDeliveryEnabled,
                IsTakeawayEnabled = input.IsTakeawayEnabled,
                ScheduleOrderSlotDuration = input.ScheduleOrderSlotDuration
            };
            var insertedOrderSetup = await _orderSetupRepository.InsertAsync(orderSetup,true);
            return ObjectMapper.Map<OrderSetup, OrderSetupDto>(insertedOrderSetup);
        }

        public async Task<OrderSetupDto> UpdateAsync(int id, CreateUpdateOrderSetupDto input)
        {
            var orderSetup = await _orderSetupRepository.GetAsync(id);
            if (orderSetup == null)
                throw new EntityNotFoundException(typeof(OrderSetup), id);
            orderSetup.BasicDeliveryCharge = input.BasicDeliveryCharge;
            orderSetup.ChargePerKilo = input.ChargePerKilo;
            orderSetup.FoodPreparationTime = input.FoodPreparationTime;
            orderSetup.FreeDeliveryKilometer = input.FreeDeliveryKilometer;
            orderSetup.IsDeliveryEnabled = input.IsDeliveryEnabled;
            orderSetup.IsTakeawayEnabled = input.IsTakeawayEnabled;
            orderSetup.ScheduleOrderSlotDuration = input.ScheduleOrderSlotDuration;
           orderSetup.LastModificationTime = DateTime.Now;
            //ObjectMapper.Map(input, orderSetup);
            var updatedOrderSetup = await _orderSetupRepository.UpdateAsync(orderSetup,true);
            return ObjectMapper.Map<OrderSetup, OrderSetupDto>(updatedOrderSetup);
        }

        public async Task<OrderSetupDto> GetByIdAsync(int id)
        {
            var orderSetup = await _orderSetupRepository.GetAsync(id);
            return ObjectMapper.Map<OrderSetup, OrderSetupDto>(orderSetup);
        }

        public async Task<PagedResultDto<OrderSetupDto>> GetListAsync(GetOrderSetupInput input)
        {
            var queryable = await _orderSetupRepository.GetQueryableAsync();
            queryable = queryable.WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                os => os.FoodPreparationTime.ToString().Contains(input.Filter) ||
                      os.ScheduleOrderSlotDuration.ToString().Contains(input.Filter));

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
            await _orderSetupRepository.DeleteAsync(id);
        }
    }
}
