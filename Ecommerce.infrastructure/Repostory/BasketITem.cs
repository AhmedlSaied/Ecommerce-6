using AutoMapper;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.infrastructure.Repostory
{
    public class BasketITem: IBasketItem
    {
        private readonly IDatabase dataBase;

        public BasketITem(IConnectionMultiplexer connect)
        {
           dataBase=connect.GetDatabase();
            
        }
        public async Task<CustomerItem?> CreateAndUpdateBasketItem(CustomerItem basket, TimeSpan? leaveTime, CancellationToken ct = default)
        {
           var basketSerlize=JsonSerializer.Serialize(basket);
           var Data =await dataBase.StringSetAsync(basket.Id, basketSerlize, leaveTime ?? TimeSpan.FromDays(7));
           return Data ? basket : null;
        }

        public async Task<bool> DeleteBaseket(string basketID, CancellationToken ct = default)
        {
            var result= await dataBase.KeyDeleteAsync(basketID);
            return result;
        }

        public async Task<CustomerItem?> GetBasketItem(string basketID, CancellationToken ct = default)
        {
            var basket = await dataBase.StringGetAsync(basketID);
            return basket.IsNullOrEmpty? null : JsonSerializer.Deserialize<CustomerItem?>(basket!);
        }
    }
}
