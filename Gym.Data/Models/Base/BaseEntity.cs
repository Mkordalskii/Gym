using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gym.Data.Models.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        [MaxLength(100)]
        public string? CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [MaxLength(100)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        [MaxLength(100)]
        public string? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
