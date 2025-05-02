using DebantErp.Dtos;
using DebantErp.Rdos;

namespace DebantErp.BL.Employee
{
    public interface IEmployeeSpecialityAssignment
    {
        Task<int> Create(CreateEmployeeSpecialityAssignmentDto dto);
        Task<EmployeeSpecialityAssignmentRdo> Get(int id);
        Task<List<EmployeeSpecialityAssignmentRdo>> GetByEmployeeId(int employeeId);
    }
}
