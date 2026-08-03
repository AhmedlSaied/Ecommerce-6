using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;
using Ecommerce.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.infrastructure.Repostory
{
    public class UniteOfWork(StoreDbContext context) : IUniteWork
    {
        private readonly Dictionary<string, object> _Repos = [];

        public IGenericRpeo<TEntity, TKey> GetRepositor<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            Console.WriteLine($"this is Type{typeName} ");

            if (_Repos.TryGetValue(typeName, out object OldRepos))
                return (IGenericRpeo<TEntity, TKey>)OldRepos;

            var NewRepo = new GenericRepositor<TEntity, TKey>(context);
            _Repos[typeName] = NewRepo;
            return NewRepo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await context.SaveChangesAsync(ct);
        }
    }
}
