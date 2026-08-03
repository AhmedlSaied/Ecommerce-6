using Ecommerce.Application.common.Results;
using Ecommerce.Application.Dtos.BasketItemDto;
using Ecommerce.Domain.Entities;


namespace Ecommerce.Application.contracts
{
    public interface IBasketItemService
    {
        Task<Result<CustomerBasketDto>> GetBasketItem(string basketID, CancellationToken ct = default);
        public Task<Result<CustomerBasketDto>> CreateAndUpdateBasketItem(CustomerBasketDto basket, TimeSpan? leaveTime, CancellationToken ct = default);


        public Task<Result<bool>> DeleteBaseket(string basketID, CancellationToken ct = default);
    }
}
