using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.Models.CartsDomain;

namespace Wajba.Configurations
{
    public class CartItemExtraConfigurations : IEntityTypeConfiguration<CartItemExtra>
    {
        public void Configure(EntityTypeBuilder<CartItemExtra> builder)
        {
            builder.ConfigureByConvention();

            builder.Property(e => e.AdditionalPrice)
                .HasColumnType("decimal(18, 2)");
            builder.ToTable("CartItemExtra");
        }
    }
}
