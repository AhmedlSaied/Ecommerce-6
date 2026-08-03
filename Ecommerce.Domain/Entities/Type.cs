using Ecommerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class ProductType :BaseEntity<int> , ISoftDeletable
    {
 
        public string Name { get; set; } = default!;
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAtUtc { get ; set ; }
    }


   
}
