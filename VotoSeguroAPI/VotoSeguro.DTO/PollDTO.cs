using System.ComponentModel.DataAnnotations;

namespace VotoSeguro.DTO
{
    public class PollDTO
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
