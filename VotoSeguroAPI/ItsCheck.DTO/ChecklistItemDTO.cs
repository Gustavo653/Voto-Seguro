using System.ComponentModel.DataAnnotations;

namespace ItsCheck.DTO
{
    public class ChecklistItemDTO
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int AmountRequired { get; set; }
        [Required]
        public int IdCategory { get; set; }
        [Required]
        public int IdItem { get; set; }
        [Required]
        public int IdChecklist { get; set; }
    }
}