using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotoSeguro.Domain
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public void SetCreatedAt()
        {
            CreatedAt = DateTime.Now;
            SetUpdatedAt();
        }
        public void SetUpdatedAt()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}
