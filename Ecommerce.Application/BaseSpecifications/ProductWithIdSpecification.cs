using Ecommerce.Application.Dtos.ProductDtos;
using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.BaseSpecifications
{
    public class ProductWithIdSpecification :BaseSpecifications<Product,int>
    {
        public ProductWithIdSpecification(IEnumerable<int> ProductsId):base(P=> ProductsId.Contains(P.Id))
        {
            
        }
    }
}
