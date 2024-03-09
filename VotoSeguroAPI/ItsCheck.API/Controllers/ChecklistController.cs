using ItsCheck.Domain.Enum;
using ItsCheck.DTO;
using ItsCheck.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItsCheck.API.Controllers
{
    public class ChecklistController(IChecklistService checklistService) : BaseController
    {
        private readonly IChecklistService _checklistService = checklistService;

        [HttpPost("")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> CreateChecklist([FromBody] ChecklistDTO checklistDTO)
        {
            var checklist = await _checklistService.Create(checklistDTO);
            return StatusCode(checklist.Code, checklist);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> UpdateChecklist([FromRoute] int id, [FromBody] ChecklistDTO checklistDTO)
        {
            var checklist = await _checklistService.Update(id, checklistDTO);
            return StatusCode(checklist.Code, checklist);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> RemoveChecklist([FromRoute] int id)
        {
            var checklist = await _checklistService.Remove(id);
            return StatusCode(checklist.Code, checklist);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetChecklists()
        {
            var checklist = await _checklistService.GetList();
            return StatusCode(checklist.Code, checklist);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var checklist = await _checklistService.GetById(id);
            return StatusCode(checklist.Code, checklist);
        }
    }
}