using AutoMapper;
using Ecommerce.Application.common.Results;
using Ecommerce.Application.contracts;
using Ecommerce.Application.Dtos.BasketItemDto;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;


namespace Ecommerce.Application.Services
{
    public class BasketItemService : IBasketItemService
    {
        private readonly IBasketItem _basketItem;
        private readonly IMapper _mapper;

        public BasketItemService(IBasketItem basketItem, IMapper mapper)
        {
            _basketItem = basketItem;
            _mapper = mapper;
        }
        public async Task<Result<CustomerBasketDto>> CreateAndUpdateBasketItem(CustomerBasketDto basket, TimeSpan? leaveTime, CancellationToken ct = default)
        {
            var basketData = _mapper.Map<CustomerBasketDto, CustomerItem>(basket);
            var result = await _basketItem.CreateAndUpdateBasketItem(basketData, leaveTime, ct);
            return (result is not null) ? Result<CustomerBasketDto>.Success(basket) : Result<CustomerBasketDto>.Failure(new Error("400", "This Id Is Not Exist",ErrorType.NotFound)) ;
        }

        public async Task<Result<bool>> DeleteBaseket(string basketID, CancellationToken ct = default)
        {
            var result=await _basketItem.DeleteBaseket(basketID, ct);
            return result ? Result<bool>.Success(true) : Result<bool>.Failure(new Error("401", "This Id Is Not Exist", ErrorType.NotFound));
        }

        public async Task<Result<CustomerBasketDto>> GetBasketItem(string basketID, CancellationToken ct = default)
        {
            var result = await _basketItem.GetBasketItem(basketID, ct);
            var Data = _mapper.Map<CustomerItem, CustomerBasketDto>(result!);
            return result is not null ? Result<CustomerBasketDto>.Success(Data) : Result<CustomerBasketDto>.Failure(new Error("401", "This Id Is Not Exist", ErrorType.NotFound));
        }
    }
}
