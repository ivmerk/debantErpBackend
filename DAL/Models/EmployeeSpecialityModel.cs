namespace DebantErp.DAL.Models
{
    public class EmployeeSpecialityModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int SpecialityId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
