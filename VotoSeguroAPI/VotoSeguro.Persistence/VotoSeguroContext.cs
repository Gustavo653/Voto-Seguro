using VotoSeguro.Domain;
using VotoSeguro.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace VotoSeguro.Persistence
{
    public class VotoSeguroContext(DbContextOptions<VotoSeguroContext> options, IHttpContextAccessor httpContextAccessor) : IdentityDbContext<User>(options)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public DbSet<Category> Categories { get; set; }

        private Guid? GetTenantId()
        {
            Session.TryGetValue(Consts.ClaimTenantId, out byte[]? tenantId);

            if (Guid.TryParse(Encoding.UTF8.GetString(tenantId!), out Guid userId))
                return userId;

            return null;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(x =>
            {
                x.HasIndex(a => new { a.Name, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<Tenant>(x =>
            {
                x.HasIndex(a => a.Name).IsUnique();
            });
        }
    }
}
