using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Gym.Data.Models.Base;

namespace Gym.Data.Models.Cms
{
    public class PortalPage : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string Slug { get; set; }
        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }
        [Required]
        public required string Content { get; set; }
        [Required]
        public bool IsPublished { get; set; } = true;
    }
}
