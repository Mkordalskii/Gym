using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Gym.Data.Models.Base;

namespace Gym.Data.Models.Core
{
    public class Booking : BaseEntity
    {
        [Required]
        public int MemberId { get; set; }
        [ForeignKey(nameof(MemberId))]
        public Member? Member { get; set; }
        [Required]
        public int FitnessClassId { get; set; }
        [ForeignKey(nameof(FitnessClassId))]
        public FitnessClass? FitnessClass { get; set; }
        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Booked";
    }
}
