using AutoMapper;
using Ecommerce.Application.BaseSpecifications;
using Ecommerce.Application.common.Results;
using Ecommerce.Application.contracts;
using Ecommerce.Application.Dtos.ProductDtos;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;


namespace Ecommerce.Application.Services
{
    public class ProductServices : IProductService
    {
        private readonly IUniteWork _unitwork;
        private readonly IMapper _mapper;


        public ProductServices(IUniteWork unitwork, IMapper mapper)
        {
            _unitwork = unitwork;
            _mapper = mapper;
        }
        public async Task<Result<ProductDtos>> AddProductAsync(ProductDtos product, CancellationToken cancelToken = default)
        {
            var productMapped =  _mapper.Map<Product>(product);
            _unitwork.GetRepositor<Product, int>().AddAsync(productMapped,cancelToken);
            var result =await _unitwork.SaveChangesAsync(cancelToken);
            if (result <= 0)
            {
                return Result<ProductDtos>.Failure(new Error("400", "product not add", ErrorType.Forbidden));
            }

            return Result<ProductDtos>.Success(product);
        }

        public void DeleteProductAsync(int id, CancellationToken cancelToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<PaginationResult<ProductDtos>>> GetAllProductAsync(int? typeId,int? brandId,string? nameOfProduct,ProductSortOprtions sort,int pageIndex,int pageSize,bool isPagination
            ,CancellationToken cancelToken = default)
        {
            var spacificat = new ProductSpecification(
                typeId:typeId,
                brandId:brandId,
                nameOfProduct:nameOfProduct,
                productSortOption:sort,
                pageSize:pageSize,
                pageIndex:pageIndex,
                IsPagination:isPagination
                );
            var countSpacification = new ProductCountSpaCification(typeId,brandId,nameOfProduct);
            var countData = await _unitwork.GetRepositor<Product, int>().CountOfItemAsync(countSpacification, cancelToken);
            var products = await _unitwork.GetRepositor<Product,int>().GetAllSpecificterAsync(spacificat, cancelToken);
            var mappedProducts= _mapper.Map<IReadOnlyList<ProductDtos>>(products);
            var Data = new PaginationResult<ProductDtos>(pageSize, pageIndex,countData, mappedProducts);
            return Result<PaginationResult<ProductDtos>>.Success(Data);
        }

      

        public async Task<Result<IReadOnlyList<BrandDtos>>> GetAllProductBrandsAsync(CancellationToken cancelToken = default)
        {
            var brands = await _unitwork.GetRepositor<Brand, int>().GetAllAsync(cancelToken);
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandDtos>>(brands);
            return Result<IReadOnlyList<BrandDtos>>.Success(mappedBrands);
        }

        public async Task<Result<IReadOnlyList<TypeDtos>>> GetAllProductTypesAsync(CancellationToken cancelToken = default)
        {
            var types = await _unitwork.GetRepositor<ProductType, int>().GetAllAsync(cancelToken);
            var mappedTypes= _mapper.Map< IReadOnlyList<ProductType>, IReadOnlyList<TypeDtos>>(types);
            return Result<IReadOnlyList<TypeDtos>>.Success(mappedTypes);
        }

        public async Task<Result<ProductDtos>> GetProductByIdAsync(int id, CancellationToken cancelToken = default)
        {
            var spacificat = new ProductSpecification(id);

            var productById =await _unitwork.GetRepositor<Product,int>().GetByIdSpecification(spacificat, cancelToken);
            var mappedProduct=_mapper.Map<ProductDtos>(productById);
            return Result<ProductDtos>.Success(mappedProduct);
        }

        public void UpdateProductAsync(ProductDtos product, CancellationToken cancelToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
