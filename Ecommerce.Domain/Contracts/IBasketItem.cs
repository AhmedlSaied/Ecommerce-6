using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts
{
    public interface IBasketItem
    {

        public Task<CustomerItem?> GetBasketItem(string basketID, CancellationToken ct = default);
        public Task<CustomerItem?> CreateAndUpdateBasketItem(CustomerItem basket,TimeSpan? leaveTime, CancellationToken ct = default);


        public Task<bool> DeleteBaseket(string basketID, CancellationToken ct = default);
    }
}
