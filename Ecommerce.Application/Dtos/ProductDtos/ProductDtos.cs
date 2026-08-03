using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dtos.ProductDtos
{
    public class ProductDtos
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = default!;
        public string BrandName { get; set; }
        public string TypeName { get; set; }
    }
}
