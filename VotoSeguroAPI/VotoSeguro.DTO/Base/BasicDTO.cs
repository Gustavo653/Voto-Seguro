using System.ComponentModel.DataAnnotations;

namespace VotoSeguro.DTO.Base
{
    public class BasicDTO
    {
        [Required]
        public required string Name { get; set; }
    }
}