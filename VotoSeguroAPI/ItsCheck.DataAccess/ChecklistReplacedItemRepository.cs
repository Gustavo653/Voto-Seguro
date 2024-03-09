using ItsCheck.Domain;
using ItsCheck.Infrastructure.Repository;
using ItsCheck.Persistence;
using Microsoft.AspNetCore.Http;

namespace ItsCheck.DataAccess
{
    public class ChecklistReplacedItemRepository : TenantBaseRepository<ChecklistReplacedItem, ItsCheckContext>, IChecklistReplacedItemRepository
    {
        public ChecklistReplacedItemRepository(IHttpContextAccessor httpContextAccessor, ItsCheckContext context) : base(httpContextAccessor, context)
        {
        }
    }
}
