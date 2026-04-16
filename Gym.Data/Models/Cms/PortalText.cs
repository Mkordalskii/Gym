using System.ComponentModel.DataAnnotations;
using Gym.Data.Models.Base;

namespace Gym.Data.Models.Cms
{
    //class to store key-value pairs for portal text, allowing for easy updates and localization
    public class PortalText : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public required string Key { get; set; }
        [Required]
        public required string Value { get; set; }
        public string Language { get; set; } = "en"; // Default to English, can be used for localization
    }
}
