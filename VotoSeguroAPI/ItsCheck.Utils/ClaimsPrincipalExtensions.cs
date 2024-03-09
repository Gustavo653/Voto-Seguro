using System.Security.Claims;

namespace ItsCheck.Utils
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string? GetUserTenant(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.PrimaryGroupSid)?.Value;
        }

        public static string? GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static string? GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}