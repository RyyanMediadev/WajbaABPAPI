global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Volo.Abp.EntityFrameworkCore.Modeling;
global using Wajba.Models.CategoriesDomain;
using Wajba.Models.UsersDomain;

namespace Wajba.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<APPUser>
{
    public void Configure(EntityTypeBuilder<APPUser> builder)
    {

        builder.ToTable("APPUser");
    }

    
}
