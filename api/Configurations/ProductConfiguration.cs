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

            builder.HasMany(a => a.Items)
                .WithOne(a => a.Product)
                .HasForeignKey(a => a.ProductId);
        }
    }
}