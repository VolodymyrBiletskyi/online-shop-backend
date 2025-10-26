using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.User)
                .WithMany(a => a.Orders)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.UserId);
        }
    }
}