using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Gym.Data.Models.Base;

namespace Gym.Data.Models.Core
{
    public class FitnessClass : BaseEntity
    {
        [Required(ErrorMessage = "Class title is required")]
        [MaxLength(100)]
        public required string Title { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        [Range(15, 240)]
        public int DurationInMinutes { get; set; }
        [Required]
        [MaxLength(20)]
        public required string Room { get; set; }
        [Required]
        [Range(1, 100)]
        public int Capacity { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
