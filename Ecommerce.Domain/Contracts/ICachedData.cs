using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts
{
    public interface ICachedData
    {
        Task<string?> GetCachedData(string Id, CancellationToken ct = default);
       
        Task SetCachedData(string Id,string value,TimeSpan ? liveTime, CancellationToken ct = default);
    }
}
