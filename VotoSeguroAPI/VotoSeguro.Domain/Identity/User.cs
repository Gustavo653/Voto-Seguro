using Microsoft.AspNetCore.Identity;

namespace VotoSeguro.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public required string Name { get; set; }
        public int? TenantId { get; set; }
        public Tenant? Tenant { get; set; }
        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}