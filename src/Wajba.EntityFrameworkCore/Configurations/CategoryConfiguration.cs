global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Volo.Abp.EntityFrameworkCore.Modeling;
global using Wajba.Models.CategoriesDomain;

namespace Wajba.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ConfigureByConvention();
       builder.Property(c => c.Status).HasConversion<int>();
        builder.ToTable("categories");
    }
}
