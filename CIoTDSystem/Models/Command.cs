using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIoTDSystem.Models
{
    public class Command
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("id_device")]
        public int DeviceId { get; set; }

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

        public Device Device { get; set; } = null!;
    }
}
