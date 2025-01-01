global using Wajba.Models.OTPDomain;

namespace Wajba.Configurations
{
    public class OTPConfiguration : IEntityTypeConfiguration<OTP>
    {
        public void Configure(EntityTypeBuilder<OTP> builder)
        {
            builder.ToTable("OTPs");
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.DigitLimit).IsRequired();
            builder.Property(x => x.ExpiryTimeInMinutes).IsRequired();
        }
    }
}