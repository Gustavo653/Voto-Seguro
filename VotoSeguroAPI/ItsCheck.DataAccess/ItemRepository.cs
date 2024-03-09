using ItsCheck.Domain;
using ItsCheck.Infrastructure.Repository;
using ItsCheck.Persistence;
using Microsoft.AspNetCore.Http;

namespace ItsCheck.DataAccess
{
    public class ItemRepository : TenantBaseRepository<Item, ItsCheckContext>, IItemRepository
    {
        public ItemRepository(IHttpContextAccessor httpContextAccessor, ItsCheckContext context) : base(httpContextAccessor, context)
        {
        }
    }
}
