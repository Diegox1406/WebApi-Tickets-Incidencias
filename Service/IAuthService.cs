using webApiTickets.DTOs;

namespace webApiTickets.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> Login(LoginDto loginDto);
    Task<AuthResponseDto?> Register(RegisterDto registerDto);
    string GenerateJwtToken(int userId, string email, string rol);
}
