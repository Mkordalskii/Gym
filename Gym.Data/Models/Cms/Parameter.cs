using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gym.Data.Models.Base;

namespace Gym.Data.Models.Cms
{
    public class Parameter : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;
        [Required]
        public string Value { get; set; } = null!;
        [MaxLength(250)]
        public string? Description { get; set; }
        [Required]
        public int ParameterCategoryId { get; set; }
        [ForeignKey(nameof(ParameterCategoryId))]
        public ParameterCategory? ParameterCategory { get; set; }
    }
}
