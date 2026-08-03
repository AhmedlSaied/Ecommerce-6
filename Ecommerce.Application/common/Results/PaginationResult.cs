using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.common.Results
{
    public class PaginationResult<TEntity>
    {
        public PaginationResult(int pageSize, int pageIndex, int itemCount, IReadOnlyList<TEntity> items)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            ItemCount = itemCount;
            Items = items;
        }

        public int PageSize { get; }
        public int PageIndex { get; }
        public int ItemCount { get;  }
        public IReadOnlyList<TEntity> Items { get; }
    }
}
