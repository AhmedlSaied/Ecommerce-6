using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dtos.AutenticationDtos
{
    public class UserDtos
    {
        public string DisplayName { get; set; } = default!;
        public string Email { get; set; }=default!;
        public string Token { get; set; } = default!;
    }
}
