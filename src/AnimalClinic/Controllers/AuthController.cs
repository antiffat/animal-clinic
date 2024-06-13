using AnimalClinic.DTOs;
using AnimalClinic.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimalClinic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.RegisterUserAsync(registerUserDto);
        if (!result)
        {
            return BadRequest("User already exists.");
        }

        return Ok("User registered successfully!");
    }
}