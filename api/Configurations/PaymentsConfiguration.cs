using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations
{
    public class PaymentsConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Order)
                .WithMany(a => a.Payments)
                .HasForeignKey(a => a.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.ProviderId).IsUnique(false);
            builder.HasIndex(x => new { x.Provider, x.ProviderId }).IsUnique(true);
        }
    }
}