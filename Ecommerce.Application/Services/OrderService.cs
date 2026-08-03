using AutoMapper;
using Ecommerce.Application.BaseSpecifications;
using Ecommerce.Application.common.Results;
using Ecommerce.Application.contracts;
using Ecommerce.Application.Dtos.OrderDtos;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketItemService _basketItemService;
        private readonly IUniteWork _uniteOfWork;
        private readonly IMapper _mapper;
        public OrderService(IBasketItemService basketItemService, IUniteWork uniteOfWork, IMapper mapper)
        {
            _basketItemService = basketItemService;
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;


        }


        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDtos orderDtos, string email, CancellationToken ct = default)
        {
            var basket = await _basketItemService.GetBasketItem(orderDtos.BasketId);
            if (basket is null)
            { 
                return Result<OrderToReturnDto>.Failure(new Error("404", "Basket Item Not Found", ErrorType.NotFound)); 
            }
            if (basket.Value.Items.Count == 0)
            {
                return Result<OrderToReturnDto>.Failure(new Error("400", "Basket Items Not Exist", ErrorType.Validation));
            }
            var orderItem = new List<ItemOfOrder>();
            var productIds = basket.Value.Items.Select(item => item.Id);
            var products=(await _uniteOfWork.GetRepositor<Product,int>().GetAllSpecificterAsync(new ProductWithIdSpecification(productIds),ct)).ToDictionary(x=>x.Id);

            foreach (var item in basket.Value.Items)
            {
                if (!products.TryGetValue(item.Id, out var product))
                    return Result<OrderToReturnDto>.Failure(new Error("404", "This Product Not Found", ErrorType.NotFound));


                orderItem.Add
                    (
                      new ItemOfOrder()
                      {
                          Price = item.Price,
                          Quantity = item.Quantity,
                          ProductOfOrderItem=new ProductOfOrderItem()
                          {
                              PictureUrl = item.PictureUrl,
                              ProductId = item.Id,
                              ProductName=product.Name,
                          }
                      }
                    );
            }

            var orderAddress = _mapper.Map<ShipingAddress>(orderDtos.ShipingAddress);
            var DeliveryMethod = await _uniteOfWork.GetRepositor<DeliveryMethod, int>().GetById(orderDtos.DeliveryMethodId,ct);
            if ( DeliveryMethod is null)
            {
                return Result<OrderToReturnDto>.Failure(new Error("404", "Delivery Method Not Found", ErrorType.NotFound));
            }
            var subTotal = orderItem.Sum(x => x.Quantity * x.Price);
            var order = new Order()
            { 
                UserEmail=email,
                shipingAddress=orderAddress,
                SubTotal=subTotal,
                DeliveryMethod=DeliveryMethod,
                items=orderItem
            };
            var repo = _uniteOfWork.GetRepositor<Order, Guid>();
            repo.AddAsync(order,ct);
            var result = await _uniteOfWork.SaveChangesAsync();
            if (result == 0)
            {
                return Result<OrderToReturnDto>.Failure(new Error("500", "Order Not Added", ErrorType.Failure));
            }
            else
            {
                await _basketItemService.DeleteBaseket(orderDtos.BasketId, ct);
                var successOrder= _mapper.Map<OrderToReturnDto>(order);
                return Result<OrderToReturnDto>.Success(successOrder);
            }
        }

        public async Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllProductForUserAsync(string email, CancellationToken ct = default)
        {
            var orders = await _uniteOfWork.GetRepositor<Order, Guid>().GetAllSpecificterAsync(new OrderSpecifications(email), ct);

            if (orders.Any())
            {
                return Result<IReadOnlyList<OrderToReturnDto>>.Success(
                    _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
            }
            else
            {
                return Result<IReadOnlyList<OrderToReturnDto>>.Failure(new Error("404", "Order Not Found", ErrorType.NotFound));
            }
        }

        public async Task<Result<OrderToReturnDto>> GetOrderByIdAndEmailForUserAsync(Guid id, string email, CancellationToken ct = default)
        {
            var orders = await _uniteOfWork.GetRepositor<Order, Guid>().GetByIdSpecification(new OrderSpecifications(id,email), ct);
            if(orders is null)
            {
                return Result<OrderToReturnDto>.Failure(new Error("404", "Order Not Found", ErrorType.NotFound));
            }
            else
            {
                return Result<OrderToReturnDto>.Success(
                  _mapper.Map<OrderToReturnDto>(orders));
            }
        }
    }
}
