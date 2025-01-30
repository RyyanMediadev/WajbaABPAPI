namespace Wajba.Configurations;

public class CartItemVariationConfigurations : IEntityTypeConfiguration<CartItemVariation>
{
    public void Configure(EntityTypeBuilder<CartItemVariation> builder)
    {
        builder.ConfigureByConvention();

        builder.Property(e => e.AdditionalPrice)
            .HasColumnType("decimal(18, 2)");
        builder.ToTable("CartItemVariations");
    }
}
