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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            //builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .IsRequired(false)
                .HasMaxLength(2000);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.PictureUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasQueryFilter(p => p.IsDeleted==false);
            //builder.HasOne(p => p.Brand)
            //   .WithMany(b => b.Products)
            //   .HasForeignKey(p => p.BrandId)
            //   .OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(p => p.Type)
            // .WithMany(b => b.Products)
            // .HasForeignKey(p => p.TypeId)
            // .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
