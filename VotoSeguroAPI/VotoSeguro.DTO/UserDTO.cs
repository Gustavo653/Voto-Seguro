using VotoSeguro.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace VotoSeguro.DTO
{
    public class UserDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public string? Password { get; set; }
        [Required]
        public required string Name { get; set; }
        public int? Coren { get; set; }
        public int? IdTenant { get; set; }
        [Required]
        public required RoleName Role { get; set; }
    }
}