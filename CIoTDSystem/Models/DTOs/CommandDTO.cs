using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CIoTDSystem.Models.DTOs
{
    public class CommandDTO
    {
        [Required]
        [StringLength(60)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column("parameters")]
        public string Parameters { get; set; } = string.Empty;

        [Required]
        [Column("return_type")]
        public string ReturnType { get; set; } = string.Empty;
    }
}
