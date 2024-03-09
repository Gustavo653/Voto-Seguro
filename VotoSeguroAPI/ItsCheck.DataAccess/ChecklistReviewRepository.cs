using ItsCheck.Domain;
using ItsCheck.Infrastructure.Repository;
using ItsCheck.Persistence;
using Microsoft.AspNetCore.Http;

namespace ItsCheck.DataAccess
{
    public class ChecklistReviewRepository : TenantBaseRepository<ChecklistReview, ItsCheckContext>, IChecklistReviewRepository
    {
        public ChecklistReviewRepository(IHttpContextAccessor httpContextAccessor, ItsCheckContext context) : base(httpContextAccessor, context)
        {
        }
    }
}
