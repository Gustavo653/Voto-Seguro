using Microsoft.AspNetCore.Http;
using VotoSeguro.Domain;
using VotoSeguro.Infrastructure.Repository;
using VotoSeguro.Persistence;

namespace VotoSeguro.DataAccess
{
    public class PollRepository(IHttpContextAccessor httpContextAccessor, VotoSeguroContext context) :
                 TenantBaseRepository<Poll, VotoSeguroContext>(httpContextAccessor, context), IPollRepository
    {
    }
}
