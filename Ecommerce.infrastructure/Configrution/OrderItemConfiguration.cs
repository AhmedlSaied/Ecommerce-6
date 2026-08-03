using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.infrastructure.Configrution
{
    public sealed class ItemOfOrderConfiguration
     : IEntityTypeConfiguration<ItemOfOrder>
    {
        public void Configure(EntityTypeBuilder<ItemOfOrder> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(item => item.Id);

            builder.Property(item => item.Price)
                .IsRequired()
                .HasPrecision(8, 2);

            builder.Property(item => item.Quantity)
                .IsRequired();

            builder.OwnsOne(
                item => item.ProductOfOrderItem,
                productBuilder =>
                {
                    productBuilder.Property(product => product.ProductId)
                        .HasColumnName("ProductId")
                        .IsRequired();

                    productBuilder.Property(product => product.ProductName)
                        .HasColumnName("ProductName")
                        .HasMaxLength(100)
                        .IsRequired();

                    productBuilder.Property(product => product.PictureUrl)
                        .HasColumnName("PictureUrl")
                        .HasMaxLength(500)
                        .IsRequired();
                });

            builder.Navigation(item => item.ProductOfOrderItem)
                .IsRequired();
        }
    }
}
