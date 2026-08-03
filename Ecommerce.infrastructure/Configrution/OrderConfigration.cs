using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Configurations
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.SubTotal).HasColumnType("decimal(8,2)");
            builder.Property(o => o.status).HasConversion<string>().HasMaxLength(50);
            builder.HasOne(order => order.DeliveryMethod)
                     .WithMany()
                     .HasForeignKey(order => order.DeliveryMethodId)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o=>o.items).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(o => o.shipingAddress, address =>
            {
                address.Property(a => a.FirstName).HasMaxLength(50);
                address.Property(a => a.LastName).HasMaxLength(50);
                address.Property(a => a.City).HasMaxLength(50);
                address.Property(a => a.Street).HasMaxLength(50);
            });
        }
    }
}
