using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts.Specification
{
    public interface ISpecification<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        public List<Expression<Func<TEntity, object>>> Includes { get; }
        public Expression<Func<TEntity, bool>>  Criteria { get; }
        public Expression<Func<TEntity, object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDecending { get; }
        public int Skip { get; }
        public int Take { get; }
        public bool IsPagingEnabled { get; }
    }
}
