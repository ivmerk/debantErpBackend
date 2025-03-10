using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebantErp.DAL.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }

        [Required, Column("first_name")]
        public string FirstName { get; set; } = null!;

        [Required, Column("middle_name")]
        public string MiddleName { get; set; } = null!;

        [Required, Column("last_name")]
        public string LastName { get; set; } = null!;

        [Required, Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Phone { get; set; } = null!;

        [Required, Column("tax_code")]
        public string TaxCode { get; set; } = null!;

        [Required]
        public GenderEnum Gender { get; set; }

        [Column("is_actual")]
        public bool IsActual { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
