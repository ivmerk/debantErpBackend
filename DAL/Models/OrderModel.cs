namespace DebantErp.DAL.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public string Number { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
