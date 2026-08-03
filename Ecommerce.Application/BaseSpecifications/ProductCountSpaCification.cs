using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.BaseSpecifications
{
    public class ProductCountSpaCification : BaseSpecifications<Product,int>
    {
        public ProductCountSpaCification(int? typeId, int? brandId, string? nameOfProduct) : base
            (
                 p => (!brandId.HasValue || p.BrandId == brandId) && (!typeId.HasValue || p.TypeId == typeId) && (string.IsNullOrEmpty(nameOfProduct) || p.Name.ToLower().Contains(nameOfProduct.ToLower()))
            )
        { }
    }
}
