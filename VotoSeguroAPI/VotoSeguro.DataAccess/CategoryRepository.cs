using VotoSeguro.Domain;
using VotoSeguro.Infrastructure.Repository;
using VotoSeguro.Persistence;
using Microsoft.AspNetCore.Http;

namespace VotoSeguro.DataAccess
{
    public class CategoryRepository : TenantBaseRepository<Category, VotoSeguroContext>, ICategoryRepository
    {
        public CategoryRepository(IHttpContextAccessor httpContextAccessor, VotoSeguroContext context) : base(httpContextAccessor, context)
        {
        }
    }
}
