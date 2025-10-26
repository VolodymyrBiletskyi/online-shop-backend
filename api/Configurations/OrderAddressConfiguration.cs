using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations
{
    public class OrderAddressConfiguration : IEntityTypeConfiguration<OrderAddress>
    {
        public void Configure(EntityTypeBuilder<OrderAddress> builder)
        {
            builder.ToTable("OrderAdresses");

            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Order)
                .WithOne(a => a.OrderAddress)
                .HasForeignKey<OrderAddress>(a => a.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}