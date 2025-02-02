﻿namespace Wajba.Configurations;

public class PopularItemConfigurations : IEntityTypeConfiguration<PopularItem>
{
    public void Configure(EntityTypeBuilder<PopularItem> builder)
    {
        builder.ConfigureByConvention();


        builder.Property(d => d.CurrentPrice)
        .HasColumnType("decimal(18, 2)");
        builder.HasMany(p => p.PopulartItemBranches)
            .WithOne(p => p.PopularItem);

        builder.Property(e => e.PrePrice)
            .HasColumnType("decimal(18, 2)");
        // An Item belongs to one category
        builder.ToTable("PopularItems");
    }
}
