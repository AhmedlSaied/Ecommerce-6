using Ecommerce.infrastructure.Identity.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.infrastructure.Identity.Data
{
    public class DbIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public DbIdentityContext(DbContextOptions<DbIdentityContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

       
    }
}
