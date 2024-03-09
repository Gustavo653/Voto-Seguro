using VotoSeguro.Domain;
using VotoSeguro.Domain.Identity;
using VotoSeguro.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace VotoSeguro.Persistence
{
    public class VotoSeguroContext : IdentityDbContext<User, Role, int,
                                               IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                               IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public VotoSeguroContext(DbContextOptions<VotoSeguroContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected VotoSeguroContext()
        {
        }

        public DbSet<Category> Categories { get; set; }

        private int? GetTenantId()
        {
            Session.TryGetValue(Consts.ClaimTenantId, out byte[]? tenantId);

            if (int.TryParse(Encoding.UTF8.GetString(tenantId!), out int userId))
                return userId;

            return null;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(x =>
            {
                x.HasKey(ur => new { ur.UserId, ur.RoleId });

                x.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                x.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

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
