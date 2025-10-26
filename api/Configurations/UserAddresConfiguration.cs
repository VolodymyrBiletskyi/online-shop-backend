using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations
{
    public class UserAddresConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("UserAddresses");

            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.User)
                .WithMany(a => a.Addres)
                .HasForeignKey(a => a.UserId);

            builder.HasIndex(a => a.UserId).IsUnique();
        }
    }
}