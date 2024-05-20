using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CIoTDSystem.Models.DTOs
{
    public class DeviceDTO
    {
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
        public ICollection<CommandDTO> Commands { get; set; } = new List<CommandDTO>();
    }
}
