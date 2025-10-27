using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");

            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Product)
                .WithMany(a => a.Variants)
                .HasForeignKey(a => a.ProductId);

            builder.Property(a => a.Attributes)
                .HasColumnType("jsonb")
                .HasDefaultValueSql("'{}'::jsonb");

            
        }
    }
}