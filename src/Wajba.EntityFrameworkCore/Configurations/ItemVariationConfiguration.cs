global using Wajba.Models.ItemVariationDomain;

namespace Wajba.Configurations;

public class ItemVariationConfiguration : IEntityTypeConfiguration<ItemVariation>
{
    public void Configure(EntityTypeBuilder<ItemVariation> builder)
    {
        builder.ConfigureByConvention();
       
        builder.Property(e => e.AdditionalPrice)
            .HasColumnType("decimal(18, 2)");

        builder.HasOne(e => e.Item)
            .WithMany(i => i.ItemVariations)
            .HasForeignKey(e => e.ItemId);

        builder.HasOne(e => e.ItemAttributes)
            .WithMany(a => a.ItemVariations)
            .HasForeignKey(e => e.ItemAttributesId);

        builder.ToTable("ItemVariations");
    }
}
