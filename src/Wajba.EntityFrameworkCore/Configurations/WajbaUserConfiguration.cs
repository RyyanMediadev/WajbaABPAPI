global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Volo.Abp.EntityFrameworkCore.Modeling;
global using Wajba.Models.CategoriesDomain;

namespace Wajba.Configurations;

public class WajbaUserConfiguration : IEntityTypeConfiguration<WajbaUser>
{
    public void Configure(EntityTypeBuilder<WajbaUser> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable("WajbaUsers");
    }

    
}
