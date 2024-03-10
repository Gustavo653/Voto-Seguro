using VotoSeguro.Domain;
using VotoSeguro.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace VotoSeguro.DataAccess
{
    public abstract class TenantBaseRepository<TType, TContext>(IHttpContextAccessor httpContextAccessor, TContext context) : 
                          BaseRepository<TType, TContext>(context) where TType : TenantBaseEntity 
                                                                   where TContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public override Task InsertAsync(TType entity)
        {
            entity.SetCreatedAt();
            Session.TryGetValue(Consts.ClaimTenantId, out byte[]? tenantId);
            entity.TenantId = Guid.Parse(Encoding.UTF8.GetString(tenantId!));
            return base.InsertAsync(entity);
        }

        public override void Update(TType entity)
        {
            entity.SetUpdatedAt();
            base.Update(entity);
        }
    }
}