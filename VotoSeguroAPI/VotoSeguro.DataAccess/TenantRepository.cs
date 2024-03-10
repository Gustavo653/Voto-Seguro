using VotoSeguro.Domain;
using VotoSeguro.Infrastructure.Repository;
using VotoSeguro.Persistence;

namespace VotoSeguro.DataAccess
{
    public class TenantRepository(VotoSeguroContext context) : 
                 BaseRepository<Tenant, VotoSeguroContext>(context), ITenantRepository
    {
    }
}
