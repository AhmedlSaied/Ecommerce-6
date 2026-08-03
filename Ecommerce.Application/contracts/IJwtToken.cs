using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.contracts
{
    public interface IJwtToken
    {
        public string CreateToken(string email , string userName , string id, IReadOnlyList<string> Roles, CancellationToken cancellationToken=default!);
    }
}
