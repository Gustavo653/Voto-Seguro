using ItsCheck.Domain;
using ItsCheck.Infrastructure.Repository;
using ItsCheck.Persistence;
using Microsoft.AspNetCore.Http;

namespace ItsCheck.DataAccess
{
    public class CategoryRepository : TenantBaseRepository<Category, ItsCheckContext>, ICategoryRepository
    {
        public CategoryRepository(IHttpContextAccessor httpContextAccessor, ItsCheckContext context) : base(httpContextAccessor, context)
        {
        }
    }
}
