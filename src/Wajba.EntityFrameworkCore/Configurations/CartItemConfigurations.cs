using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wajba.Models.Carts;

namespace Wajba.Configurations
{
    public class CartItemConfigurations : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ConfigureByConvention();



            builder
    .HasMany(ci => ci.SelectedExtras)
    .WithOne(e => e.CartItem)
    .HasForeignKey(e => e.CartItemId)
    .OnDelete(DeleteBehavior.Cascade); // Enables cascade delete

            builder
              .HasMany(ci => ci.SelectedAddons)
                .WithOne(e => e.CartItem)
                .HasForeignKey(e => e.CartItemId)
                .OnDelete(DeleteBehavior.Cascade); // Enables cascade delete

            builder
 .HasMany(ci => ci.SelectedVariations)
    .WithOne(e => e.CartItem)
    .HasForeignKey(e => e.CartItemId)
    .OnDelete(DeleteBehavior.Cascade); // Enables cascade delete


            builder.Property(e => e.price)
             .HasColumnType("decimal(18, 2)");


            builder.ToTable("CartItems");
        }
    }
}