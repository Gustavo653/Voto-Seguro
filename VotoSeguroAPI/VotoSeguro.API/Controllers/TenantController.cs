using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VotoSeguro.Domain.Enum;
using VotoSeguro.DTO.Base;
using VotoSeguro.Infrastructure.Service;

namespace VotoSeguro.API.Controllers
{
    public class TenantController(ITenantService tenantService) : BaseController
    {
        private readonly ITenantService _tenantService = tenantService;

        [HttpPost("")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> CreateTenant([FromBody] BasicDTO name)
        {
            var item = await _tenantService.Create(name);
            return StatusCode(item.Code, item);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> UpdateTenant([FromRoute] Guid id, [FromBody] BasicDTO name)
        {
            var item = await _tenantService.Update(id, name);
            return StatusCode(item.Code, item);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> RemoveTenant([FromRoute] Guid id)
        {
            var item = await _tenantService.Remove(id);
            return StatusCode(item.Code, item);
        }

        [HttpGet("")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> GetTenants()
        {
            var item = await _tenantService.GetList();
            return StatusCode(item.Code, item);
        }
    }
}