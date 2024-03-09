using ItsCheck.DTO.Base;
using System.ComponentModel.DataAnnotations;

namespace ItsCheck.DTO
{
    public class ChecklistDTO : BasicDTO
    {
        [Required]
        public required bool RequireFullReview { get; set; }
        [Required]
        public virtual required IEnumerable<CategoryDTO> Categories { get; set; }
    }

    public class CategoryDTO
    {
        [Required]
        public required int Id { get; set; }
        [Required]
        public virtual required IEnumerable<ItemDTO> Items { get; set; }
    }

    public class ItemDTO
    {
        [Required]
        public required int Id { get; set; }
        [Required]
        public required int AmountRequired { get; set; }
        public List<ItemDTO>? SubItems { get; set; }
    }
}