namespace ItsCheck.Domain
{
    public class ChecklistItem : TenantBaseEntity
    {
        public required int RequiredQuantity { get; set; }
        public int ItemId { get; set; }
        public required virtual Item Item { get; set; }
        public int CategoryId { get; set; }
        public required virtual Category Category { get; set; }
        public int ChecklistId { get; set; }
        public required virtual Checklist Checklist { get; set; }
        public int? ParentChecklistItemId { get; set; }
        public virtual ChecklistItem? ParentChecklistItem { get; set; }
        public virtual List<ChecklistItem>? ChecklistSubItems { get; set; }
        public virtual List<ChecklistReplacedItem>? ChecklistReplacedItems { get; set; }
    }
}
