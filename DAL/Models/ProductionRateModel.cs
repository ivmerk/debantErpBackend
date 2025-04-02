using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebantErp.DAL.Models
{
    public class ProductionRateModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductionOperation")]
        [Column("production_operation_id")]
        public int? ProductionOperationId { get; set; }

        [Column("is_actual")]
        public bool IsActual { get; set; } = true;

        [Required]
        [Column("operation_timeframe")]
        public int OperationTimeframe { get; set; }

        [Required]
        [Column("rate")]
        public decimal Rate { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public ProductionOperationModel? ProductionOperation { get; set; }
    }
}
