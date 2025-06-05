using DAL.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Services;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using ReservationAPI.DTOs.Auth;

namespace ReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TW1DbContext context;
        private readonly AuthService jwtService;

        public AuthController(TW1DbContext context, AuthService jwtService)
        {
            this.context = context;
            this.jwtService = jwtService;
        }

        [HttpPost("Register")]
        //public async Task<IActionResult> RegisterAsync(LoginDto dto)
        //{

        //}

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var user = await context.AuthUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid Credentials");

            var token = jwtService.GenerateJwtToken(user);
            var refreshToken = jwtService.GenerateRefreshToken(user);

            return Ok(new { token, refreshToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync(RefreshTokenDto dto)
        {
            var user = await context.AuthUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.RefreshToken == dto.RefreshToken);

            if (user == null)
                return Unauthorized("Invalid refresh token");

            var token = jwtService.GenerateJwtToken(user);
            var refreshToken = jwtService.GenerateRefreshToken(user);

            return Ok(new { token, refreshToken });
        }
    }
}
