using Ecommerce.Application.BaseSpecifications;
using Ecommerce.Application.common.Results;
using Ecommerce.Application.Dtos.ProductDtos;
using Ecommerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.contracts
{
    public interface IProductService 
    {
        public Task<Result<PaginationResult<ProductDtos>>> GetAllProductAsync(int? typeId, int? brandId, string? nameOfProduct, ProductSortOprtions sort, int pageIndex, int pageSize, bool isPagination
            , CancellationToken cancelToken = default);
        public Task<Result<IReadOnlyList<BrandDtos>>> GetAllProductBrandsAsync(CancellationToken cancelToken = default);
        public Task<Result<IReadOnlyList<TypeDtos>>> GetAllProductTypesAsync(CancellationToken cancelToken=default);
        public Task<Result<ProductDtos>> GetProductByIdAsync(int id, CancellationToken cancelToken = default);

        public Task<Result<ProductDtos>> AddProductAsync(ProductDtos product, CancellationToken cancelToken = default);
        public void UpdateProductAsync(ProductDtos product, CancellationToken cancelToken = default);
        public void DeleteProductAsync(int id, CancellationToken cancelToken = default);
    }
}
