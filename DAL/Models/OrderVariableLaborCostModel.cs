namespace DebantErp.DAL.Models
{
    public class OrderVariableLaborCostModel
    {
        public int Id { get; set; }

        public EmployeeModel Employee { get; set; } = null!;

        public ProductionRateModel ProductionRate { get; set; } = null!;

        public int Quantity { get; set; }

        public OrderModel Order { get; set; } = null!;

        public decimal Cost { get; set; }
    }
}
