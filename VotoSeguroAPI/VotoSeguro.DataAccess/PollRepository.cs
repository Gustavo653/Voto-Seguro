using VotoSeguro.Domain;
using VotoSeguro.Infrastructure.Repository;
using VotoSeguro.Persistence;
using Microsoft.AspNetCore.Http;

namespace VotoSeguro.DataAccess
{
    public class PollRepository(IHttpContextAccessor httpContextAccessor, VotoSeguroContext context) : 
                 TenantBaseRepository<Poll, VotoSeguroContext>(httpContextAccessor, context), IPollRepository
    {
    }
}
