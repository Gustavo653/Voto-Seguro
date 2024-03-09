using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;

namespace ItsCheck.Utils
{
    public class TenantMiddleware : IMiddleware
    {
        private readonly ILogger<TenantMiddleware> _logger;

        public TenantMiddleware(ILogger<TenantMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var logId = Guid.NewGuid();

            context.Request.EnableBuffering();
            string requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
            context.Request.Body.Position = 0;

            _logger.LogInformation("Request {RequestId}, LogId {LogId}, Remote IP {RemoteIpAddress}", context.TraceIdentifier, logId, context.Connection.RemoteIpAddress);
            _logger.LogInformation("Request Body {Body}", requestBody);
            _logger.LogInformation("Headers {Headers}", context.Request.Headers);
            context.Session.Set(Consts.LogId, Encoding.UTF8.GetBytes(logId.ToString()));

            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userId = GetClaimValue(context.User, ClaimTypes.NameIdentifier);
                var tenantId = GetClaimValue(context.User, ClaimTypes.PrimaryGroupSid);
                var email = GetClaimValue(context.User, ClaimTypes.Email);
                context.Session.Set(Consts.ClaimUserId, Encoding.UTF8.GetBytes(userId));
                context.Session.Set(Consts.ClaimTenantId, Encoding.UTF8.GetBytes(tenantId));
                context.Session.Set(Consts.ClaimEmail, Encoding.UTF8.GetBytes(email));

                _logger.LogInformation("Tenant {TenantId}, User {UserId}", tenantId, userId);
            }
            await next(context);
        }

        private static string GetClaimValue(ClaimsPrincipal user, string claimType)
        {
            var claim = user.Claims.FirstOrDefault(x => x.Type == claimType);
            return claim?.Value ?? string.Empty;
        }
    }
}