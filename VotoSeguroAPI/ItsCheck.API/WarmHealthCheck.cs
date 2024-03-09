using ItsCheck.Domain.Identity;
using ItsCheck.Infrastructure.Repository;
using ItsCheck.Infrastructure.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

namespace ItsCheck.API
{
    public class WarmHealthCheck(IAccountService accountService,
                                 IAmbulanceService ambulanceService,
                                 ICategoryService categoryService,
                                 IChecklistService checklistService,
                                 IChecklistReviewService checklistReviewService,
                                 IItemService itemService,
                                 ITenantService tenantService,
                                 ITokenService tokenService,
                                 IUserRepository userRepository,
                                 IAmbulanceRepository ambulanceRepository,
                                 ICategoryRepository categoryRepository,
                                 IChecklistRepository checklistRepository,
                                 IChecklistReviewRepository checklistReviewRepository,
                                 IItemRepository itemRepository,
                                 ITenantRepository tenantRepository,
                                 SignInManager<User> signInManager,
                                 UserManager<User> userManager,
                                 IHttpContextAccessor httpContextAccessor) : IHealthCheck
    {
        private readonly IAccountService accountService = accountService;
        private readonly IAmbulanceService ambulanceService = ambulanceService;
        private readonly ICategoryService categoryService = categoryService;
        private readonly IChecklistService checklistService = checklistService;
        private readonly IChecklistReviewService checklistReviewService = checklistReviewService;
        private readonly IItemService itemService = itemService;
        private readonly ITenantService tenantService = tenantService;
        private readonly ITokenService tokenService = tokenService;

        private readonly IUserRepository userRepository = userRepository;
        private readonly IAmbulanceRepository ambulanceRepository = ambulanceRepository;
        private readonly ICategoryRepository categoryRepository = categoryRepository;
        private readonly IChecklistRepository checklistRepository = checklistRepository;
        private readonly IChecklistReviewRepository checklistReviewRepository = checklistReviewRepository;
        private readonly IItemRepository itemRepository = itemRepository;
        private readonly ITenantRepository tenantRepository = tenantRepository;

        private readonly SignInManager<User> signInManager = signInManager;
        private readonly UserManager<User> userManager = userManager;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var tenants = await tenantService.GetList();
            Log.Information("Healthcheck executando  {code}", tenants.Code);
            return HealthCheckResult.Healthy();
        }
    }
}
