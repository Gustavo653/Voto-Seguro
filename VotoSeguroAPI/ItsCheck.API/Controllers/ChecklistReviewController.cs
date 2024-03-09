using ItsCheck.Domain.Enum;
using ItsCheck.DTO;
using ItsCheck.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItsCheck.API.Controllers
{
    public class ChecklistReviewController(IChecklistReviewService checklistReviewService) : BaseController
    {
        private readonly IChecklistReviewService _checklistReviewService = checklistReviewService;

        [HttpPost("")]
        [Authorize(Roles = $"{nameof(RoleName.Employee)}, {nameof(RoleName.Manager)}")]
        public async Task<IActionResult> CreateChecklistReview([FromBody] ChecklistReviewDTO checklistReviewDTO)
        {
            var checklistReview = await _checklistReviewService.Create(checklistReviewDTO);
            return StatusCode(checklistReview.Code, checklistReview);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> UpdateChecklistReview([FromRoute] int id, [FromBody] ChecklistReviewDTO checklistReviewDTO)
        {
            var checklistReview = await _checklistReviewService.Update(id, checklistReviewDTO);
            return StatusCode(checklistReview.Code, checklistReview);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(RoleName.Manager))]
        public async Task<IActionResult> RemoveChecklistReview([FromRoute] int id)
        {
            var checklistReview = await _checklistReviewService.Remove(id);
            return StatusCode(checklistReview.Code, checklistReview);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetChecklistReviews([FromQuery] int? takeLast)
        {
            var checklistReview = await _checklistReviewService.GetList(takeLast);
            return StatusCode(checklistReview.Code, checklistReview);
        }
    }
}