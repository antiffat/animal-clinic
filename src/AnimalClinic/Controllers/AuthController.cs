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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (IsSuccess, AccessToken, RefreshToken) = await _userService.LoginUserAsync(loginUserDto);
        if (!IsSuccess)
        {
            return Unauthorized("Invalid username or password");
        }

        return Ok(new { AccessToken, RefreshToken });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto refreshTokenDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (IsSuccess, AccessToken) = await _userService.RefreshTokenAsync(refreshTokenDto);
        if (!IsSuccess)
        {
            return Unauthorized("Invalid refresh token");
        }
        
        return Ok(new { AccessToken });
    }
}