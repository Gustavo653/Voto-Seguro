using System.ComponentModel.DataAnnotations;

namespace ItsCheck.DTO
{
    public class ChecklistReplacedItemDTO
    {
        [Required]
        public int AmountReplaced { get; set; }
        [Required]
        public int IdChecklistItem { get; set; }
        [Required]
        public int IdChecklistReview { get; set; }
    }
}