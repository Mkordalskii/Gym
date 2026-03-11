using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Gym.Data.Models.Base;

namespace Gym.Data.Models.Core
{
    public class MembershipPlan : BaseEntity
    {
        [Required(ErrorMessage = "Membership plan name is required")]
        [MaxLength(100)]
        public required string Name { get; set; }
        [Required]
        [Range(1,3650)]
        [Display(Name = "Duration (days)")]
        public int DurationInDays { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    }
}
