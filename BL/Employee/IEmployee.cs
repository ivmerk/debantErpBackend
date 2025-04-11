using DebantErp.Dtos;
using DebantErp.Rdos;

namespace DebantErp.BL.Employee
{
    public interface IEmployee
    {
        public Task<int> CreateEmployee(CreateEmployeeDto dto);
        public Task<int> UpdateEmployee(int id, UpdateEmployeeDto dto);
        public Task<int> DeleteEmployee(int id);
        public Task<EmployeeRdo> GetEmployee(int id);
        public Task<IEnumerable<EmployeeRdo>> GetEmployees();
    }
}
