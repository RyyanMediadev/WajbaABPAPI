namespace Wajba.Configurations;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ConfigureByConvention();

        builder.Property(e => e.DiscountPercentage)
        .HasColumnType("decimal(18, 2)");

        builder.HasMany(p => p.OfferCategories)
               .WithOne(p => p.Offer)
               .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
        builder.HasMany(p => p.OfferItems)
            .WithOne(p => p.Offer)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

        builder.ToTable("offers");
    }
}
