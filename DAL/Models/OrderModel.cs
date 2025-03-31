using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebantErp.DAL.Models
{
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }

        [Required, Column("number")]
        public string Number { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
