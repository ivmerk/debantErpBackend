using DebantErp.BL.Employee;
using DebantErp.Dtos;
using DebantErp.Rdos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployee _employee;
    private readonly IEmployeeDetails _employeeDetails;

    public EmployeeController(IEmployee employee, IEmployeeDetails employeeDetails)
    {
        _employee = employee;
        _employeeDetails = employeeDetails;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<EmployeeRdo>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetList()
    {
        var users = await _employee.Get();
        if (users == null) return NotFound();
        return Ok(users);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeRdo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _employee.Get(id);
        return Ok(user);
    }

    [HttpGet("details/{employee_id}")]
    [ProducesResponseType(typeof(EmployeeDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetails(int employee_id)
    {
        var employeeDetails = await _employeeDetails.GetEmployeeDetailsByEmployeeId(employee_id);
        return Ok(employeeDetails);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
        //        await _auth.ValidateEmail(dto.Email ?? "");
        //
        var userId = await _employee.Create(dto);
        return Ok(userId);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
    {
        if (dto.LastName != null || dto.MiddleName != null || dto.FirstName != null)
        {
            var userId = await _employee.Update(id, dto);
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
    public async Task<IActionResult> Delete(int id)
    {
        var userId = await _employee.Delete(id);
        return Ok(userId);
    }
}
