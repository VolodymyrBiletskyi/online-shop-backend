using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");
            
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Cart)
                .WithMany(a => a.Items)
                .HasForeignKey(a => a.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.CartId);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);

            builder.HasIndex(x => x.VariantId);

            builder.HasOne(x => x.ProductVariant)
                .WithMany()
                .HasForeignKey(x => x.VariantId);

            builder.HasIndex(x => x.VariantId);
        }
    }
}