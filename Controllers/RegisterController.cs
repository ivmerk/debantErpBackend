using DebantErp.BL.Auth;
using DebantErp.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("register")]
public class RegisterController : ControllerBase
{
    private readonly IAuth _auth;

    public RegisterController(IAuth auth)
    {
        _auth = auth;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
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
        var userId = await _auth.CreateUser(dto);
        return Ok(userId);
    }

    /*[HttpGet("{id}")]*/
    /*public async Task<IActionResult> GetUser(int id)*/
    /*{*/
    /*    var user = await _auth.GetUser(id);*/
    /*    return Ok(user);*/
    /*}*/
}
