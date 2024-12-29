using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.Models.CartsDomain;

namespace Wajba.Configurations
{
    public class CartItemAddonsConfigurations : IEntityTypeConfiguration<CartItemAddon>
    {
        public void Configure(EntityTypeBuilder<CartItemAddon> builder)
        {
            builder.ConfigureByConvention();
            builder.Property(e => e.AdditionalPrice)
                .HasColumnType("decimal(18, 2)");
            builder.ToTable("CartItemAddon");
        }
    }
}
