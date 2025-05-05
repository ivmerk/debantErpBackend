using Microsoft.AspNetCore.Mvc;
using DebantErp.Dtos;
using DebantErp.Rdos;
using DebantErp.BL.Employee;
using DebantErp.BL.Speciality;

namespace DebantErp.Controllers
{
  [ApiController]
  [Route("api/assignment")]
  public class EmployeeAssignmentController:ControllerBase
  {

    private readonly IEmployeeSpecialityAssignment _employeeSpecialityAssignment;
    private readonly IEmployee _employee;
    private readonly ISpeciality _speciality;

    public EmployeeAssignmentController(IEmployeeSpecialityAssignment employeeSpecialityAssignment, IEmployee employee, ISpeciality speciality)
    {
      _employeeSpecialityAssignment = employeeSpecialityAssignment;
      _employee = employee;
      _speciality = speciality;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeSpecialityAssignmentRdo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAssignment(int id)
    {
      var assignments = await _employeeSpecialityAssignment.Get(id);
      return Ok(assignments);
    }

    [HttpGet("list/{employeeId}")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeSpecialityAssignmentRdo>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListByEmployee(int employeeId)
    {
      var assignments = await _employeeSpecialityAssignment.GetByEmployee(employeeId);
      return Ok(assignments);
    }

    [HttpPost()]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAssignment([FromBody] CreateEmployeeAssignmentDto dto)
    {
      var assignmentId = await _employeeSpecialityAssignment.Create(dto);
      return Ok(assignmentId);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAssignment(int id, [FromBody] UpdateEmployeeAssignmentDto dto)
    {
      var assignmentId = await _employeeSpecialityAssignment.Update(id, dto);
      return Ok(assignmentId);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAssignment(int id)
    {
      await _employeeSpecialityAssignment.Delete(id);
      return Ok();
    }
  }
}
