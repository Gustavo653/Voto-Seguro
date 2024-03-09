using ItsCheck.Domain;
using ItsCheck.Infrastructure.Repository;
using ItsCheck.Persistence;
using Microsoft.AspNetCore.Http;

namespace ItsCheck.DataAccess
{
    public class AmbulanceRepository : TenantBaseRepository<Ambulance, ItsCheckContext>, IAmbulanceRepository
    {
        public AmbulanceRepository(IHttpContextAccessor httpContextAccessor, ItsCheckContext context) : base(httpContextAccessor, context)
        {
        }
    }
}
