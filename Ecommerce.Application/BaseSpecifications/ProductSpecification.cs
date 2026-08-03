using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.BaseSpecifications
{
    public class ProductSpecification :BaseSpecifications<Product,int>
    {
        public ProductSpecification(int? typeId,int? brandId, string? nameOfProduct , ProductSortOprtions? productSortOption,int? pageSize,int?pageIndex,bool? IsPagination=false)
            :base
            (
                 p=>(!brandId.HasValue||p.BrandId==brandId) && (!typeId.HasValue||p.TypeId==typeId) && (string.IsNullOrEmpty(nameOfProduct) || p.Name.ToLower().Contains(nameOfProduct.ToLower()))
            )
        {
            AddInclude(x => x.Type);
            AddInclude(x => x.Brand);

            switch (productSortOption)
            {
                case ProductSortOprtions.PriceAscending:
                    AddOrderByAsyndec(p => p.Price);
                    break;
                case ProductSortOprtions.PriceDescending:
                    AddOrderByDecendc(p => p.Price);
                    break;
                case ProductSortOprtions.NameDescending:
                    AddOrderByDecendc(p => p.Name);  
                    break;
                case ProductSortOprtions.NameAscending:
                    AddOrderByAsyndec(p => p.Name);
                    break;
                default:
                    AddOrderByAsyndec(p => p.Id);
                    break;

            }
         
                EnablePagination(pageSize: pageSize ?? 10, pageIndex: pageIndex ?? 1,IsPagination:IsPagination ?? false);
            
          
        }
        public ProductSpecification(int id):base(x=>x.Id==id)
        {
            AddInclude(x => x.Type);
            AddInclude(x => x.Brand);
        }
    }
}
