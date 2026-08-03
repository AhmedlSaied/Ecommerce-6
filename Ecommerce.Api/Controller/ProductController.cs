using Ecommerce.Api.Attrbuite;
using Ecommerce.Application.BaseSpecifications;
using Ecommerce.Application.common.Results;
using Ecommerce.Application.contracts;
using Ecommerce.Application.Dtos.ProductDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ApiBaseController
    {
        [HttpGet]
        [ApplicationCacheData]
        public async Task<ActionResult<PaginationResult<ProductDtos>>> GetAllProducts([FromQuery] int? typeId, [FromQuery] int? brandId,string? nameOfProduct , [FromQuery] ProductSortOprtions sort ,int pageSize, [FromQuery] int pageIndex, [FromQuery] bool IsPagination ,CancellationToken token = default)
        {
            var product = await productService.GetAllProductAsync(typeId: typeId,brandId: brandId,nameOfProduct: nameOfProduct,sort: sort,pageIndex: pageIndex,pageSize: pageSize,IsPagination, token);
            var result = ToActionResult
                (product);
            return result;

        }
        [HttpGet("Brand")]
        public async Task<ActionResult<IReadOnlyList<BrandDtos>>> GetAllBrand(CancellationToken token = default)
        {
            var Brand = await productService.GetAllProductBrandsAsync(token);
            var result = ToActionResult(Brand);
            return result;

        }
        [HttpGet("Type")]
        public async Task<ActionResult<IReadOnlyList<TypeDtos>>> GetAllType(CancellationToken token = default)
        {
            var types = await productService.GetAllProductTypesAsync(token);
            var result = ToActionResult(types);
            return result;

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDtos>> GetProductById([FromQuery] int id, CancellationToken token = default)
        {
            var product = await productService.GetProductByIdAsync(id, token);
            var result = ToActionResult(product);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDtos>> AddProduct([FromBody] ProductDtos  product,CancellationToken token = default)
        {
            return ToActionResult(await productService.AddProductAsync(product, token));
        }
    }
}
 