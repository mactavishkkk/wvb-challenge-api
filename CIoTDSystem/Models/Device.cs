using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CIoTDSystem.Models
{
    public class Device
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(7)]
        [Column("identifier")]
        public string Identifier { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column("manufacturer")]
        public string Manufacturer { get; set; } = string.Empty;

        [Required]
        [Column("url")]
        public string Url { get; set; } = string.Empty;

        [Required]
        [Column("status")]
        public bool Status { get; set; } = false;

        [Required]
        [Column("commands")]
        public ICollection<Command> Commands { get; set; } = new List<Command>();

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
