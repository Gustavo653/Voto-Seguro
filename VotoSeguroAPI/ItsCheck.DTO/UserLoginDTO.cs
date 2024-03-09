using System.ComponentModel.DataAnnotations;

namespace ItsCheck.DTO
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}