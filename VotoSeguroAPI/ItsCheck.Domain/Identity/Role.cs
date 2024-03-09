using Microsoft.AspNetCore.Identity;

namespace ItsCheck.Domain.Identity
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}