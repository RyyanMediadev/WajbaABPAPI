global using Wajba.Models.Items;

namespace Wajba.Configurations
{
    internal class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ConfigureByConvention();


            builder.Property(d => d.TaxValue)
            .HasColumnType("decimal(18, 2)");


            builder.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)");
            // An Item belongs to one category
            builder.HasOne(item => item.Category)
            .WithMany(category => category.Items)
            .HasForeignKey(item => item.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);


            // An Item has many ItemAddons
            builder.HasMany(e => e.ItemAddons) 
             .WithOne(ia => ia.Item);

            builder.HasMany(i => i.ItemBranches)
                 .WithOne(ib => ib.Item)
                 .HasForeignKey(ib => ib.ItemId);
            
            // An Item has many ItemExtras
            builder.HasMany(e => e.ItemExtras) 
            .WithOne(ia => ia.Item);

            builder.HasMany(i => i.ItemVariations)
            .WithOne(iv => iv.Item)
            .HasForeignKey(iv => iv.ItemId);
            builder.ToTable("Items");
        }
    }
}
