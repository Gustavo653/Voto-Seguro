using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text;
using VotoSeguro.Domain;
using VotoSeguro.Utils;

namespace VotoSeguro.Persistence
{
    public class VotoSeguroContext(DbContextOptions<VotoSeguroContext> options, IHttpContextAccessor httpContextAccessor) : IdentityDbContext<User>(options)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
        public DbSet<PollVote> PollVotes { get; set; }

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

            modelBuilder.Entity<Poll>(x =>
            {
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<PollVote>(x =>
            {
                x.HasIndex(a => new { a.UserId, a.TenantId, a.PollId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<PollOption>(x =>
            {
                x.HasIndex(a => new { a.Title, a.TenantId, a.PollId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<Tenant>(x =>
            {
                x.HasIndex(a => a.Name).IsUnique();
            });
        }
    }
}
