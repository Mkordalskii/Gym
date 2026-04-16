using System.ComponentModel.DataAnnotations;
using Gym.Data.Models.Base;

namespace Gym.Data.Models.Cms
{
    public class ParameterCategory : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(250)]
        public string? Description { get; set; }
        public ICollection<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}
