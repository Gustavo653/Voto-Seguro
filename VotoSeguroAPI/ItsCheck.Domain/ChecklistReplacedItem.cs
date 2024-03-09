namespace ItsCheck.Domain
{
    public class ChecklistReplacedItem : TenantBaseEntity
    {
        public required int ReplacedQuantity { get; set; } // Quantidade reposta
        public required int RequiredQuantity { get; set; } // Quantidade requirida
        public required int ReplenishmentQuantity { get; set; } // Quantidade a repor
        public int ChecklistReviewId { get; set; }
        public required virtual ChecklistReview ChecklistReview { get; set; }
        public int ChecklistItemId { get; set; }
        public required virtual ChecklistItem ChecklistItem { get; set; }
    }
}
