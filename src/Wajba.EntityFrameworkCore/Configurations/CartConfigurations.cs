global using Wajba.Models.Carts;

namespace Wajba.Configurations;

public class CartConfigurations : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ConfigureByConvention();
        builder.Property(e => e.TotalAmount)
            .HasColumnType("decimal(18, 2)");

        builder.ConfigureByConvention();
        builder.Property(e => e.SubTotal)
            .HasColumnType("decimal(18, 2)");

        builder.ConfigureByConvention();
        builder.Property(e => e.voucherCode)
            .HasColumnType("decimal(18, 2)");

        builder.ConfigureByConvention();
        builder.Property(e => e.DiscountAmount)
            .HasColumnType("decimal(18, 2)");

        builder.ConfigureByConvention();
        builder.Property(e => e.ServiceFee)
            .HasColumnType("decimal(18, 2)");

        builder.ConfigureByConvention();
        builder.Property(e => e.DeliveryFee)
        .HasColumnType("decimal(18, 2)");

        builder.HasMany(c => c.CartItems)
  .WithOne(ci => ci.cart)
  .HasForeignKey(ci => ci.CartId)
  .OnDelete(DeleteBehavior.Cascade);


        builder.ToTable("Cart");
    }
}
