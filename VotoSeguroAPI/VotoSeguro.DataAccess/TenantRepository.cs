using VotoSeguro.Domain;
using VotoSeguro.Infrastructure.Repository;
using VotoSeguro.Persistence;

namespace VotoSeguro.DataAccess
{
    public class TenantRepository : BaseRepository<Tenant, VotoSeguroContext>, ITenantRepository
    {
        public TenantRepository(VotoSeguroContext context) : base(context)
        {
        }
    }
}
