namespace VotoSeguro.Domain
{
    public abstract class TenantBaseEntity : BaseEntity
    {
        public Guid TenantId { get; set; }
    }
}
