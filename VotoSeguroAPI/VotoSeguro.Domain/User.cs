using Microsoft.AspNetCore.Identity;
using VotoSeguro.Domain.Enum;

namespace VotoSeguro.Domain
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }
        public Guid? TenantId { get; set; }
        public Tenant? Tenant { get; set; }
        public required UserRole Role { get; set; }
    }
}