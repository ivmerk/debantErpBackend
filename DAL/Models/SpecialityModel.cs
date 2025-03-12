using System.ComponentModel.DataAnnotations;

namespace DebantErp.DAL.Models
{
    public class SpecialityModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsActual { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
