using ItsCheck.Domain.Enum;
using ItsCheck.Domain.Identity;

namespace ItsCheck.Domain
{
    public class ChecklistReview : TenantBaseEntity
    {
        public required ReviewType Type { get; set; }
        public string? Observation { get; set; }
        public required virtual Ambulance Ambulance { get; set; }
        public required virtual Checklist Checklist { get; set; }
        public required virtual User User { get; set; }
        public List<ChecklistReplacedItem>? ChecklistReplacedItems { get; set; }
    }
}
