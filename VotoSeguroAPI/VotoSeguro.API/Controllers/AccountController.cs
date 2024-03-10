using VotoSeguro.Domain.Enum;
using VotoSeguro.DTO;
using VotoSeguro.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VotoSeguro.API.Controllers
{
    public class AccountController(IAccountService accountService) : BaseController
    {
        private readonly IAccountService _accountService = accountService;

        [HttpGet("Current")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _accountService.GetCurrent();
            return StatusCode(user.Code, user);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            var user = await _accountService.Login(userLogin);
            return StatusCode(user.Code, user);
        }

        [HttpGet("")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)}, {nameof(UserRole.Elector)}")]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _accountService.GetUsers();
            return StatusCode(user.Code, user);
        }

        [HttpPost("")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)}, {nameof(UserRole.Elector)}")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            var user = await _accountService.CreateUser(userDTO);
            return StatusCode(user.Code, user);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)}, {nameof(UserRole.Elector)}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserDTO userDTO)
        {
            var user = await _accountService.UpdateUser(id, userDTO);
            return StatusCode(user.Code, user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)}, {nameof(UserRole.Elector)}")]
        public async Task<IActionResult> RemoveUser([FromRoute] Guid id)
        {
            var user = await _accountService.RemoveUser(id);
            return StatusCode(user.Code, user);
        }
    }
}