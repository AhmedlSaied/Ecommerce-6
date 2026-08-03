using Microsoft.AspNetCore.Identity;

namespace Ecommerce.infrastructure.Identity.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = default!;
        public Address? UserAddress { get; set; } = default!;
    }
}
