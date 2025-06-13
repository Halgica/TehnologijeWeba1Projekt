using DAL.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Services;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using ReservationAPI.DTOs.Auth;
using AutoMapper;
using DAL.Models;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace ReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TW1DbContext context;
        private readonly AuthService jwtService;
        private readonly IMapper _mapper;

        public AuthController(TW1DbContext context, AuthService jwtService, IMapper mapper)
        {
            this.context = context;
            this.jwtService = jwtService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto dto)
        {
            var Register = _mapper.Map<AuthUser>(dto);
            Register.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            Register.Role = await context.AuthRoles.FindAsync(dto.RoleId);

            await context.AuthUsers.AddAsync(Register);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
            return Ok(Register);
        }

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

            return Ok(new { 
                token, 
                refreshToken,

            });
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
        [Authorize(Roles = "Admin")]
        [HttpPost("addRole")]
        public async Task<IActionResult> AddRole(AuthRoleDto dto)
        {
            var role = _mapper.Map<AuthRole>(dto);
            await context.AuthRoles.AddAsync(role);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
            return Ok(role);
        }
    }
}
