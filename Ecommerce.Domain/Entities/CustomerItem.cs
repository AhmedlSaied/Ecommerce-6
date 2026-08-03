using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class CustomerItem : BaseEntity<string>
    {
        public ICollection<BasketItem> Items { get; set; } = new HashSet<BasketItem>();
    }
}
