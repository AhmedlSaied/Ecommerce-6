
using Ecommerce.Application.contracts;
using Ecommerce.Application.Dtos.BasketItemDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBasket(IBasketItemService basketItemService) : ApiBaseController
    {

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetCustomerItem([FromQuery]  string id, CancellationToken ct) 
        {
            var result = await basketItemService.GetBasketItem(id, ct);
            var Data = ToActionResult(result);
            return Data!;
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCustomerItem([FromQuery]string id, CancellationToken ct)
        {
            var result = await basketItemService.DeleteBaseket(id, ct);
            var Data = ToActionResult(result);
            return Data;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdateCustomerItem([FromBody]CustomerBasketDto basket,int? NumberOfDayes, CancellationToken ct)
        {
            var result = await basketItemService.CreateAndUpdateBasketItem(basket,TimeSpan.FromDays(NumberOfDayes??7),ct);
            var Data = ToActionResult(result);
            return Data!;
        }
    }
}
