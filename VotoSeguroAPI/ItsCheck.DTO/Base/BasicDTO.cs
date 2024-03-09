using System.ComponentModel.DataAnnotations;

namespace ItsCheck.DTO.Base
{
    public class BasicDTO
    {
        [Required] public required string Name { get; set; }
    }
}