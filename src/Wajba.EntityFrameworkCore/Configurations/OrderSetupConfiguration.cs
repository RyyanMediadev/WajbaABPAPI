using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.Models.OrderSetup;

namespace Wajba.Configurations
{
    public class OrderSetupConfiguration : IEntityTypeConfiguration<OrderSetup>
    {
        public void Configure(EntityTypeBuilder<OrderSetup> builder)
        {
            builder.ToTable("OrderSetups"); 
            builder.Property(x => x.FoodPreparationTime).IsRequired();
            builder.Property(x => x.ScheduleOrderSlotDuration).IsRequired();
            builder.Property(x => x.FreeDeliveryKilometer).IsRequired();
            builder.Property(x => x.BasicDeliveryCharge).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.ChargePerKilo).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.IsTakeawayEnabled).IsRequired();
            builder.Property(x => x.IsDeliveryEnabled).IsRequired();
        }
    }
}