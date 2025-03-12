using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebantErp.DAL.Models
{
    public class EmployeeSpecialityModel
    {
        [Key]
        public int Id { get; set; }

        [Required, Column("employee_id")]
        public int EmployeeId { get; set; }

        [Required, Column("speciality_id")]
        public int SpecialityId { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
