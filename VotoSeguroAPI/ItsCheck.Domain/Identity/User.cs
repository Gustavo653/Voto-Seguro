using Microsoft.AspNetCore.Identity;

namespace ItsCheck.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public required string Name { get; set; }
        public int? Coren { get; set; }
        public int? TenantId { get; set; }
        public Tenant? Tenant { get; set; }
        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}