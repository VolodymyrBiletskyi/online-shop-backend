using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventory");

            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.ProductVariant)
                .WithMany(a => a.InventoryItems)
                .HasForeignKey(a => a.VariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}