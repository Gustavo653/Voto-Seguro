namespace ItsCheck.Domain
{
    public class Checklist : TenantBaseEntity
    {
        public required string Name { get; set; }
        public required bool RequireFullReview { get; set; }
        public virtual List<ChecklistItem>? ChecklistItems { get; set; }
    }
}
