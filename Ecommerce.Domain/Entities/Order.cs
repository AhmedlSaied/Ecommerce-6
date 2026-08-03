using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class Order:BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public ShipingAddress shipingAddress { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; }=default!;
        public ICollection<ItemOfOrder> items { get; set; } = [];
        public decimal SubTotal { get; set; } = default!;
        public decimal GetTotal()=>SubTotal + (DeliveryMethod?.Price ?? 0);

    }
}
