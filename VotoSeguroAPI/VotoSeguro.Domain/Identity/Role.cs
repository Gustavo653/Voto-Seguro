using Microsoft.AspNetCore.Identity;

namespace VotoSeguro.Domain.Identity
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}