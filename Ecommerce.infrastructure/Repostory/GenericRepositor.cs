using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Contracts.Specification;
using Ecommerce.Domain.Entities;
using Ecommerce.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.infrastructure.Repostory
{
    public class GenericRepositor<TEntity, Tkey>(StoreDbContext context) : IGenericRpeo<TEntity, Tkey>
        where TEntity : BaseEntity<Tkey>
    {
        public void AddAsync(TEntity entity, CancellationToken Ct)
        {
            context.Set<TEntity>().AddAsync(entity, Ct);
        }

        public Task<int> CountOfItemAsync(ISpecification<TEntity, Tkey> specifications, CancellationToken Ct)
        {
            return SpecificationEvaluator.GetQueryable<TEntity, Tkey>(context.Set<TEntity>(), specifications).CountAsync();
        }

        public void DeleteAsync(TEntity entity, CancellationToken Ct)
        {
           context.Set<TEntity>().Remove(entity);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken Ct)
        {
           return await context.Set<TEntity>().AsNoTracking().ToListAsync(Ct);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllSpecificterAsync(ISpecification<TEntity, Tkey> specifications, CancellationToken Ct)
        {
           var result=SpecificationEvaluator.GetQueryable<TEntity, Tkey>(context.Set<TEntity>(), specifications) ;
            return await result.ToListAsync(Ct); 
        }

        public async Task<TEntity> GetById(Tkey id, CancellationToken Ct)
        {
            return await context.Set<TEntity>().FindAsync(id, Ct);
        }

        public async Task<TEntity> GetByIdSpecification(ISpecification<TEntity, Tkey> specifications, CancellationToken Ct)
        {
           var result=SpecificationEvaluator.GetQueryable<TEntity, Tkey>(context.Set<TEntity>(), specifications);
            return await  result.FirstOrDefaultAsync(Ct);
        }

        public void UpdateAsync(TEntity entity, CancellationToken Ct)
        {
           context.Set<TEntity>().Update(entity);
        }
    }
}
