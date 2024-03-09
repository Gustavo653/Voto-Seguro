using System.ComponentModel.DataAnnotations;

namespace ItsCheck.DTO
{
    public class AmbulanceDTO
    {
        [Required]
        public required int Number { get; set; }
        [Required]
        public required string LicensePlate { get; set; }
    }
}