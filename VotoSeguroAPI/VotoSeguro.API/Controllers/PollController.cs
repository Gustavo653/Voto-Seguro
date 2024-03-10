using Microsoft.AspNetCore.Mvc;
using VotoSeguro.DTO;
using VotoSeguro.Infrastructure.Service;

namespace VotoSeguro.API.Controllers
{
    public class PollController(IPollService pollService) : BaseController
    {
        private readonly IPollService _pollService = pollService;

        [HttpPost("")]
        public async Task<IActionResult> CreatePoll([FromBody] PollDTO pollDTO)
        {
            var poll = await _pollService.Create(pollDTO);
            return StatusCode(poll.Code, poll);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePoll([FromRoute] Guid id, [FromBody] PollDTO pollDTO)
        {
            var poll = await _pollService.Update(id, pollDTO);
            return StatusCode(poll.Code, poll);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePoll([FromRoute] Guid id)
        {
            var poll = await _pollService.Remove(id);
            return StatusCode(poll.Code, poll);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetPolls()
        {
            var poll = await _pollService.GetList();
            return StatusCode(poll.Code, poll);
        }
    }
}