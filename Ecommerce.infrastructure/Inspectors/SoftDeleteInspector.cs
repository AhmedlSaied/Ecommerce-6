using Ecommerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.infrastructure.Inspectors
{
    public class SoftDeleteInspector : SaveChangesInterceptor
    {
        override public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData is null)
                return result;



            var deletedEntries = eventData.Context.ChangeTracker
                                                              .Entries<ISoftDeletable>()
                                                              .Where(entry => entry.State == EntityState.Deleted)
                                                              .ToList();
            foreach (var entry in deletedEntries)
            {
                entry.State = EntityState.Unchanged;

                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAtUtc = DateTime.UtcNow;

                entry.Property(entity => entity.IsDeleted)
                    .IsModified = true;

                entry.Property(entity => entity.DeletedAtUtc)
                    .IsModified = true;
            }

            return base.SavingChanges(eventData, result);
        }
    }
}
