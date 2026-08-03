using Ecommerce.Domain.Contracts.Specification;
using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts
{
    public interface IGenericRpeo<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
       
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken Ct);
        Task<TEntity> GetById(TKey id, CancellationToken Ct);
        void AddAsync(TEntity entity, CancellationToken Ct);
        void UpdateAsync(TEntity entity, CancellationToken Ct);
        void DeleteAsync(TEntity entity, CancellationToken Ct);
        Task<IReadOnlyList<TEntity>> GetAllSpecificterAsync(ISpecification<TEntity,TKey> specifications,CancellationToken Ct);
        Task<TEntity> GetByIdSpecification(ISpecification<TEntity, TKey> specifications, CancellationToken Ct);
        Task<int> CountOfItemAsync(ISpecification<TEntity, TKey> specifications, CancellationToken Ct);

    }
}
