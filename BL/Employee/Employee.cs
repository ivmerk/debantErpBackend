using DebantErp.DAL;
using DebantErp.Dtos;
using DebantErp.Rdos;

namespace DebantErp.BL.Employee
{
    public class Employee : IEmployee
    {
        private readonly IEmployeeDAL _employeeDAL;

        public Employee(IEmployeeDAL employeeDAL)
        {
            _employeeDAL = employeeDAL;
        }

        public Task<IEnumerable<EmployeeRdo>> GetEmployees() => throw new NotImplementedException();

        public async Task<EmployeeRdo> GetEmployee(int id)
        {
            var employee = await _employeeDAL.Get(id);

            System.Console.WriteLine(employee.Id.ToString(), employee.FirstName);

            var employeeRdo = new EmployeeRdo
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                IsActual = employee.IsActual.ToString(),
                CreatedAt = employee.CreatedAt,
                UpdatedAt = employee.UpdatedAt,
            };
            return employeeRdo;
        }

        public Task<int> CreateEmployee(CreateEmployeeDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateEmployee(int id, UpdateEmployeeDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }
    }
}
