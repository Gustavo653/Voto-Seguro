using ItsCheck.Domain;
using ItsCheck.Domain.Identity;
using ItsCheck.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ItsCheck.Persistence
{
    public class ItsCheckContext : IdentityDbContext<User, Role, int,
                                               IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                               IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public ItsCheckContext(DbContextOptions<ItsCheckContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected ItsCheckContext()
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<Ambulance> Ambulances { get; set; }
        public DbSet<ChecklistReview> ChecklistReviews { get; set; }
        public DbSet<ChecklistReplacedItem> ChecklistReplacedItems { get; set; }

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

            modelBuilder.Entity<Ambulance>(x =>
            {
                x.HasIndex(a => new { a.Number, a.LicensePlate, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<Checklist>(x =>
            {
                x.HasIndex(a => new { a.Name, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<ChecklistItem>(x =>
            {
                x.HasIndex(a => new { a.ItemId, a.CategoryId, a.ChecklistId, a.TenantId, a.ParentChecklistItemId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<Category>(x =>
            {
                x.HasIndex(a => new { a.Name, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<Item>(x =>
            {
                x.HasIndex(a => new { a.Name, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<Tenant>(x =>
            {
                x.HasIndex(a => a.Name).IsUnique();
            });

            modelBuilder.Entity<ChecklistReview>(x =>
            {
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<ChecklistReplacedItem>(x =>
            {
                x.HasIndex(a => new { a.ChecklistItemId, a.ChecklistReviewId, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });
        }
    }
}
