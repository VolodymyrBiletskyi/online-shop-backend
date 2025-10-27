using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Order)
                .WithMany(a => a.OrderItems)
                .HasForeignKey(a => a.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(a => a.ProductVariant)
                .WithMany(a => a.OrderItem)
                .HasForeignKey(a => a.VariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.OrderId);
            builder.HasIndex(a => a.VariantId);
        }
    }
}