using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebantErp.DAL.Models
{
    public class EmployeeDetailsModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("tax_code")]
        public string TaxCode { get; set; } = null!;

        [Required]
        [Column("address")]
        public string Address { get; set; } = null!;

        [Required]
        [Column("email")]
        public string Email { get; set; } = null!;

        [Required]
        [Column("phone")]
        public string Phone { get; set; } = null!;

        [Required]
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Column("gender")]
        public GenderEnum Gender { get; set; }

        [Required]
        [Column("picture")]
        public string Picture { get; set; } = null!;

        [Column("employee_id")]
        public int EmployeeId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
