using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Gym.Data.Models.Base;

namespace Gym.Data.Models.Core
{
    public class Membership : BaseEntity
    {
        [Required]
        public int MemberId { get; set; }
        [ForeignKey(nameof(MemberId))] //nameof better than "MemberId" because it will auto-update if the property name changes
        public Member? Member { get; set; }
        [Required]
        public int MembershipPlanId { get; set; }
        [ForeignKey(nameof(MembershipPlanId))]
        public MembershipPlan? MembershipPlan { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Active";
    }
}
