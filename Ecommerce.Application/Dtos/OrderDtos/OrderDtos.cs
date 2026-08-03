using Ecommerce.Application.Dtos.AutenticationDtos;
using System.ComponentModel.DataAnnotations;


namespace Ecommerce.Application.Dtos.OrderDtos
{
    public class OrderDtos
    {
        [Required]
        public string BasketId { get; set; } = default!;
        [Required]
        public int DeliveryMethodId{ get; set; }
        [Required]
        public AddressDto ShipingAddress { get; set; } = default!;
    }
}
