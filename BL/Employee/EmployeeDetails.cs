using DebantErp.DAL;
using DebantErp.DAL.Models;
using DebantErp.Dtos;
using DebantErp.Rdos;

namespace DebantErp.BL.Employee
{
    public class EmployeeDetails : IEmployeeDetails
    {
        private readonly IEmployeeDetailsDAL _employeeDetailsDAL;

        public EmployeeDetails(IEmployeeDetailsDAL employeeDetailsDAL)
        {
            _employeeDetailsDAL = employeeDetailsDAL;
        }

        public async Task<EmployeeDetailsRdo> GetEmployeeDetailsByEmployeeId(int employeeId)
        {
            var employeeDetails = await _employeeDetailsDAL.GetByEmployeeId(employeeId);

            var employeeDetailsRdo = new EmployeeDetailsRdo
            {
                Id = employeeDetails.Id,
                TaxCode = employeeDetails.TaxCode,
                Address = employeeDetails.Address,
                Email = employeeDetails.Email,
                Phone = employeeDetails.Phone,
                BirthDate = employeeDetails.BirthDate.ToString(),
                Gender = employeeDetails.Gender.ToString(),
                Picture = employeeDetails.Picture,
                EmployeeId = employeeDetails.EmployeeId,
            };
            return employeeDetailsRdo;
        }

        public async Task<int> UpdateEmployeeDetails(int employeeId, UpdateEmployeeDto dto)
        {
            var employeeDetails = await _employeeDetailsDAL.GetByEmployeeId(employeeId);

            if (dto?.TaxCode != null)
                employeeDetails.TaxCode = dto.TaxCode;
            if (dto?.Address != null)
                employeeDetails.Address = dto.Address;
            if (dto?.Email != null)
                employeeDetails.Email = dto.Email;
            if (dto?.Phone != null)
                employeeDetails.Phone = dto.Phone;
            if (dto?.BirthDate != null)
                employeeDetails.BirthDate = DateTime.Parse(dto.BirthDate);
            if (dto?.Gender != null)
                employeeDetails.Gender = dto.Gender == "male" ? GenderEnum.Male : GenderEnum.Female;

            return await _employeeDetailsDAL.Update(employeeDetails);
        }
    }
}
