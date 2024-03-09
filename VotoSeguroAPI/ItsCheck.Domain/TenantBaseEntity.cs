namespace ItsCheck.Domain
{
    public abstract class TenantBaseEntity : BaseEntity
    {
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
