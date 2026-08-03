using Ecommerce.Application.common.Results;
using Ecommerce.Application.contracts;
using Ecommerce.Application.Dtos.OrderDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDtos order , CancellationToken ct = default)
        {
            var email =  User.FindFirstValue(ClaimValueTypes.Email) ?? throw new UnauthorizedAccessException("No Email Found");
            return ToActionResult(await _orderService.CreateOrderAsync(order, email, ct));
        }
        [Authorize]
        [HttpGet("GetAllorders")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrders( CancellationToken ct = default)
        {
            var email = User.FindFirstValue(ClaimValueTypes.Email) ?? throw new UnauthorizedAccessException("No Email Found");
            return ToActionResult(await _orderService.GetAllProductForUserAsync(email));

        }
        [Authorize]
        [HttpGet("GetAllordersByID")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdAndEmailForUserAsync(Guid id,  CancellationToken ct = default)
        {
            var email = User.FindFirstValue(ClaimValueTypes.Email) ?? throw new UnauthorizedAccessException("No Email Found");
            return ToActionResult(await _orderService.GetOrderByIdAndEmailForUserAsync(id,email,ct));
        }
    }
}
