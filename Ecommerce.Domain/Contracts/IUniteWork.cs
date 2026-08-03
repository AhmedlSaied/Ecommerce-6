using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts
{
    public interface IUniteWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);

        IGenericRpeo<TEntity,TKey> GetRepositor<TEntity, TKey>() where TEntity:BaseEntity<TKey>;
    }
}
