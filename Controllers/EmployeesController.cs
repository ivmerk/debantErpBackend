using DebantErp.BL.Employee;
using DebantErp.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployee _employee;

    public EmployeesController(IEmployee employee)
    {
        _employee = employee;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Данные отсутствуют.");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        //        await _auth.ValidateEmail(dto.Email ?? "");
        var userId = await _employee.CreateEmployee(dto);
        return Ok(userId);
    }

    [HttpGet("details/{id}")]
    public Task<IActionResult> GetEmployeeDetails(int id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var user = await _employee.GetEmployee(id);
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Данные отсутствуют.");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = await _employee.UpdateEmployee(id, dto);
        return Ok(userId);
    }
}
