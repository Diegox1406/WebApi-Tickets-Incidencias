using Microsoft.AspNetCore.Mvc;
using webApiTickets.DTOs;
using webApiTickets.Services;

namespace webApiTickets.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
    {
        var result = await _authService.Login(loginDto);
        
        if (result == null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
    {
        var result = await _authService.Register(registerDto);
        
        if (result == null)
            return BadRequest(new { message = "El email ya está registrado" });

        return Ok(result);
    }
}
