using Ecommerce.Domain.Contracts;
using StackExchange.Redis;

namespace Ecommerce.infrastructure.Repostory
{
    public class CachedData: ICachedData
    {
        private readonly IDatabase _database;
        public CachedData(IConnectionMultiplexer connection)
        {
            _database=connection.GetDatabase();
        }
        public async Task<string?> GetCachedData(string Id, CancellationToken ct = default)
        {
           var result=await _database.StringGetAsync(Id);
            return !(result.IsNullOrEmpty) ? result.ToString() : null;
        }

        public async Task SetCachedData(string Id, string value, TimeSpan? liveTime, CancellationToken ct = default)
        {
            await _database.StringSetAsync(Id, value, liveTime ?? TimeSpan.FromDays(7));
        }
    }
}
