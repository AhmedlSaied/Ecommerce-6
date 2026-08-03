using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.infrastructure.Data
{
    public class StoreDbContext : DbContext
    {
      
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }
         public DbSet<Product> Products { get; set; } = default!;
         public DbSet<Brand> Brands { get; set; } = default!;
         public DbSet<ProductType> Types { get; set; } = default!;
         public DbSet<Order> Orders { get; set; }
         public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    }
}
