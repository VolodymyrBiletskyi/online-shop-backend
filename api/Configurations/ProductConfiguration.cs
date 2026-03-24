using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.HasIndex(s => s.Slug).IsUnique();
            builder.HasIndex(s => s.Sku).IsUnique();

            builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

            builder.Property(x => x.Available)
                .IsRequired();

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Sku)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(i => i.Items)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId);

            builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);
        }
    }
}