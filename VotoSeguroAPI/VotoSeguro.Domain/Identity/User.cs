using Microsoft.AspNetCore.Identity;

namespace VotoSeguro.Domain.Identity
{
    public class User : IdentityUser<Guid>
    {
        public required string Name { get; set; }
        public Guid? TenantId { get; set; }
        public Tenant? Tenant { get; set; }
        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}