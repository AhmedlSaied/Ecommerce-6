using Ecommerce.Application.contracts;
using Ecommerce.Domain.Contracts;
using System.Text.Json;

namespace Ecommerce.Application.Services
{
    internal class CachedDataService : ICachedDataService
    {
        private readonly ICachedData cached;

        public CachedDataService(ICachedData cached)
        {
            this.cached = cached;
        }
        public async Task<string?> GetCachedData(string Id, CancellationToken ct = default)
        {
           return await cached.GetCachedData(Id, ct);
        }

        public async Task SetCachedData(string Id, object value, TimeSpan? liveTime, CancellationToken ct = default)
        {
            var result = JsonSerializer.Serialize(value,new JsonSerializerOptions(){PropertyNamingPolicy=JsonNamingPolicy.CamelCase });
            await cached.SetCachedData(Id, result,liveTime, ct);
        }
    }
}
