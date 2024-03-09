using ItsCheck.Domain.Enum;
using ItsCheck.DTO;
using ItsCheck.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItsCheck.API.Controllers
{
    public class AmbulanceController(IAmbulanceService ambulanceService) : BaseController
    {
        private readonly IAmbulanceService _ambulanceService = ambulanceService;

        [HttpPost("")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> CreateAmbulance([FromBody] AmbulanceDTO ambulanceDTO)
        {
            var ambulance = await _ambulanceService.Create(ambulanceDTO);
            return StatusCode(ambulance.Code, ambulance);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> UpdateAmbulance([FromRoute] int id, [FromBody] AmbulanceDTO ambulanceDTO)
        {
            var ambulance = await _ambulanceService.Update(id, ambulanceDTO);
            return StatusCode(ambulance.Code, ambulance);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> RemoveAmbulance([FromRoute] int id)
        {
            var ambulance = await _ambulanceService.Remove(id);
            return StatusCode(ambulance.Code, ambulance);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAmbulances()
        {
            var ambulance = await _ambulanceService.GetList();
            return StatusCode(ambulance.Code, ambulance);
        }
    }
}