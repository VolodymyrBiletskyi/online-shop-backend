using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations
{
    public class OrderDisscountConfiguration : IEntityTypeConfiguration<OrderDiscount>
    {
        public void Configure(EntityTypeBuilder<OrderDiscount> builder)
        {
            builder.ToTable("OrderDiscounts");

            builder.HasKey(x => new { x.OrderId, x.CouponId });

            builder.HasOne(a => a.Order)
                .WithMany(a => a.OrderDiscount)
                .HasForeignKey(a => a.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Coupon)
                .WithMany(a => a.OrderDiscount)
                .HasForeignKey(a => a.CouponId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}