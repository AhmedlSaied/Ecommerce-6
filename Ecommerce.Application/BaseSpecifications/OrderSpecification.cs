using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.BaseSpecifications
{
    internal class OrderSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderSpecifications(string email)
            : base(order => order.UserEmail == email)
        {
            AddInclude(order => order.DeliveryMethod);
            AddInclude(order => order.items);
            AddOrderByDecendc(order => order.OrderDate);
        }
        public OrderSpecifications(Guid id, string email)
    : base(order => order.UserEmail == email && order.Id == id)
        {
            AddInclude(order => order.DeliveryMethod);
            AddInclude(order => order.items);
        }
    }
}
