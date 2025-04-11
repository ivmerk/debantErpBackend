using DebantErp.DAL;
using DebantErp.DAL.Models;
using DebantErp.Dtos;
using DebantErp.Rdos;

namespace DebantErp.BL.Employee
{
    public class Employee : IEmployee
    {
        private readonly IEmployeeDAL _employeeDAL;
        private readonly IEmployeeDetailsDAL _employeeDetailsDAL;

        public Employee(IEmployeeDAL employeeDAL, IEmployeeDetailsDAL employeeDetailsDAL)
        {
            _employeeDAL = employeeDAL;
            _employeeDetailsDAL = employeeDetailsDAL;
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

        public async Task<int> CreateEmployee(CreateEmployeeDto dto)
        {
            var employee = new EmployeeModel
            {
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
            };
            var id = await _employeeDAL.Create(employee);

            var employeeDetails = new EmployeeDetailsModel
            {
                TaxCode = dto.TaxCode,
                Address = dto.Address,
                Email = dto.Email,
                Phone = dto.Phone,
                BirthDate = DateTime.Parse(dto.BirthDate),
                Gender = dto.Gender == "male" ? GenderEnum.Male : GenderEnum.Female,
                Picture = "",
                EmployeeId = id,
            };
            await _employeeDetailsDAL.Create(employeeDetails);

            return id;
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
