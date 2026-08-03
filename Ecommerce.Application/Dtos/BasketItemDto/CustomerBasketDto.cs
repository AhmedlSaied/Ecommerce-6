using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dtos.BasketItemDto
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public ICollection<BasketItemDto> Items { get; set; } = default!;
    }
  
}
