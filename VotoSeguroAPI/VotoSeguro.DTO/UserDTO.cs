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
        public Guid? IdTenant { get; set; }
        [Required]
        public required UserRole Role { get; set; }
    }
}