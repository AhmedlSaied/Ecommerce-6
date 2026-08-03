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
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQueryable<TEntity, Tkey>
            (
            IQueryable<TEntity> inputQuery,
            ISpecification<TEntity, Tkey> specifications) where TEntity : BaseEntity<Tkey>
        {
            var query = inputQuery;
            if (specifications.Criteria is not null)
            {
                query = query.Where(specifications.Criteria);
            }

            if (specifications.Includes.Count()>0)
            {
                query = specifications.Includes.Aggregate(query, (current, nextExpression) => current.Include(nextExpression));

            }

            if(specifications.OrderBy is not null)
            {
                query=query.OrderBy(specifications.OrderBy);
            }else if(specifications.OrderByDecending is not null)
            {
                query=query.OrderByDescending(specifications.OrderByDecending);
            }

            if (specifications.IsPagingEnabled)
            {
                query=query.Skip(specifications.Skip).Take(specifications.Take);

            }
            return query;
        }
    }
}
