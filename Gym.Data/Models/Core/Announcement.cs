using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Gym.Data.Models.Base;

namespace Gym.Data.Models.Core
{
    public class Announcement : BaseEntity
    {
        [Required(ErrorMessage = "Announcement title is required")]
        [MaxLength(150)]
        public required string Title { get; set; }
        [Required(ErrorMessage = "Announcement content is required")]
        public required string Content { get; set; }
        [Required]
        [Display(Name = "Publish From")]
        public DateTime PublishFrom { get; set; } // When the announcement should start being visible
        [Display(Name = "Publish To")]
        public DateTime? PublishTo { get; set; } // Optional end date for the announcement visibility
    }
}
