using DebantErp.BL.Employee;
using DebantErp.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployee _employee;
    private readonly IEmployeeDetails _employeeDetails;

    public EmployeesController(IEmployee employee, IEmployeeDetails employeeDetails)
    {
        _employee = employee;
        _employeeDetails = employeeDetails;
    }

    [HttpGet("details/{employee_id}")]
    public async Task<IActionResult> GetEmployeeDetails(int employee_id)
    {
        var employeeDetails = await _employeeDetails.GetEmployeeDetailsByEmployeeId(employee_id);
        return Ok(employeeDetails);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var user = await _employee.GetEmployee(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        //        await _auth.ValidateEmail(dto.Email ?? "");
        var userId = await _employee.CreateEmployee(dto);
        return Ok(userId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (dto.LastName != null || dto.MiddleName != null || dto.FirstName != null)
        {
            var userId = await _employee.UpdateEmployee(id, dto);
        }
        if (
            dto.TaxCode != null
            || dto.Address != null
            || dto.Email != null
            || dto.Phone != null
            || dto.BirthDate != null
            || dto.Gender != null
        )
        {
            var userId = await _employeeDetails.UpdateEmployeeDetails(id, dto);
        }
        return Ok(id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var userId = await _employee.DeleteEmployee(id);
        return Ok(userId);
    }
}
