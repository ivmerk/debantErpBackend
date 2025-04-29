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

        public async Task<int> UpdateEmployee(int id, UpdateEmployeeDto dto)
        {
            var emplotyee = await _employeeDAL.Get(id);
            if (emplotyee == null)
            {
                return 0;
            }
            if (dto.FirstName != null)
                emplotyee.FirstName = dto.FirstName;
            if (dto.MiddleName != null)
                emplotyee.MiddleName = dto.MiddleName;
            if (dto.LastName != null)
                emplotyee.LastName = dto.LastName;
            return await _employeeDAL.Update(emplotyee);
        }

        public Task<int> DeleteEmployee(int id)
        {
            var employee = _employeeDAL.Get(id);
            if (employee == null)
            {
                return Task.FromResult(0);
            }
            return _employeeDAL.Delete(id);
        }
    }
}
