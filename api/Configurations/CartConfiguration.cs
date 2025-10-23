using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.User)
                .WithOne(a => a.Cart)
                .HasForeignKey<Cart>(a => a.UserId);

            builder.HasIndex(a => a.UserId).IsUnique();
        }
    }
}