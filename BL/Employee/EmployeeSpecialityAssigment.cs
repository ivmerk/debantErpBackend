using DebantErp.DAL;
using DebantErp.DAL.Models;
using DebantErp.Dtos;
using DebantErp.Rdos;

namespace DebantErp.BL.Employee
{
    public class EmployeeSpecialityAssigment : IEmployeeSpecialityAssignment
    {
        private readonly IEmployeeSpecialityAssignmentDAL _employeeSpecialityAssignmentDAL;

        public EmployeeSpecialityAssigment(
            IEmployeeSpecialityAssignmentDAL employeeSpecialityAssignmentDAL
        )
        {
            _employeeSpecialityAssignmentDAL = employeeSpecialityAssignmentDAL;
        }

        public Task<int> Create(CreateEmployeeSpecialityAssignmentDto dto)
        {
            var model = new EmployeeSpecialityAssignmentModel
            {
                EmployeeId = dto.EmployeeId,
                SpecialityId = dto.SpecialityId,
                DateFrom = DateTime.Parse(dto.DateFrom),
            };
            return _employeeSpecialityAssignmentDAL.Create(model);
        }

        public Task<int> Delete(int id) => _employeeSpecialityAssignmentDAL.Delete(id);

        public async Task<EmployeeSpecialityAssignmentRdo> Get(int id)
        {
            var model = await _employeeSpecialityAssignmentDAL.Get(id);
            var rdo = new EmployeeSpecialityAssignmentRdo
            {
                Id = model.Id,
                EmployeeId = model.EmployeeId,
                SpecialityId = model.SpecialityId,
                DateFrom = model.DateFrom,
            };
            return rdo;
        }

        public async Task<List<EmployeeSpecialityAssignmentRdo>> GetByEmployeeId(int employeeId)
        {
            var models = await _employeeSpecialityAssignmentDAL.GetByEmployeeId(employeeId);
            var rdos = new List<EmployeeSpecialityAssignmentRdo>();
            foreach (var model in models)
            {
                var rdo = new EmployeeSpecialityAssignmentRdo
                {
                    Id = model.Id,
                    EmployeeId = model.EmployeeId,
                    SpecialityId = model.SpecialityId,
                    DateFrom = model.DateFrom,
                };
                rdos.Add(rdo);
            }
            return rdos;
        }

        public async Task<int> Update(int id, UpdateEmployeeSpecialityAssignmentDto dto)
        {
            var assignment = await _employeeSpecialityAssignmentDAL.Get(id);
            if (assignment == null)
            {
                throw new Exception("EmployeeSpecialityAssigment not found");
            }
            if (dto.EmployeeId != null)
            {
                assignment.EmployeeId = dto.EmployeeId.Value;
            }

            if (dto.SpecialityId != null)
            {
                assignment.SpecialityId = dto.SpecialityId.Value;
            }

            if (
                !string.IsNullOrWhiteSpace(dto.DateFrom)
                && DateTime.TryParse(dto.DateFrom, out DateTime dateFrom)
            )
            {
                assignment.DateFrom = dateFrom;
            }
            return await _employeeSpecialityAssignmentDAL.Update(assignment);
        }
    }
}
