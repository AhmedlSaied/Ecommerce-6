using Ecommerce.Application.common.Results;
using Ecommerce.Application.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.contracts
{
    public interface IOrderService
    {
        Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDtos orderDtos, string email, CancellationToken ct = default);
        Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllProductForUserAsync( string email, CancellationToken ct = default);
        Task<Result<OrderToReturnDto>> GetOrderByIdAndEmailForUserAsync(Guid id, string email, CancellationToken ct = default);
    }
}


