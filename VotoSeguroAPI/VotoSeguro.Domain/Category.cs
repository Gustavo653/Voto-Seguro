namespace VotoSeguro.Domain
{
    public class Category : TenantBaseEntity
    {
        public required string Name { get; set; }
    }
}
