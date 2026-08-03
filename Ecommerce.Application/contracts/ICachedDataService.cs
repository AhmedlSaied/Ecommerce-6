using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.contracts
{
    public interface ICachedDataService
    {
        Task<string?> GetCachedData(string Id, CancellationToken ct = default);
        Task SetCachedData(string Id, object value, TimeSpan? liveTime, CancellationToken ct = default);
    }
}
