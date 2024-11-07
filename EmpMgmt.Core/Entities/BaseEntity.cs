using System.ComponentModel.DataAnnotations;

namespace EmpMgmt.Core.Entities
{
    public abstract class BaseEntity
    {
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        [MaxLength(50)]
        public string? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
