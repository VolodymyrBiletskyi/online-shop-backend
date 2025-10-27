using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Product)
                .WithMany(a => a.Images)
                .HasForeignKey(a => a.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.ProductVariant)
                .WithMany(a => a.Images)
                .HasForeignKey(a => a.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(x => new { x.ProductId, x.IsPrimary })
                .HasFilter("\"IsPrimary\" = true")
                .IsUnique();

            builder.HasIndex(x => new { x.ProductId, x.IsPrimary })
                .HasFilter("\"ProductVariantId\"  IS NOT NULL AND \"IsPrimary\" = true")
                .IsUnique();
        }
    }
}