using AutoMapper;
using DAL.Models;
using DAL.Repos.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.DTOs.Read;
using ReservationAPI.DTOs.Write;

namespace ReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #region Web methods

        [HttpGet (Name = "GetUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsersAsync([FromQuery] string? name, [FromQuery] string? email)
        {
            var users = await _userRepository.FindAsync(u =>
                (string.IsNullOrEmpty(name) || u.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(email) || u.Email.Contains(email, StringComparison.OrdinalIgnoreCase))
            );

            if (!users.Any())
                return NotFound("No users found matching the criteria.");

            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserCreateUpdateDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            try
            {
                await _userRepository.AddAsync(user);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
            return CreatedAtAction(nameof(GetUserByIdAsync).Replace("Async", ""), new { id = user.Id }, userDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync([FromBody] UserDto user)
        {
            var entity = await _userRepository.GetByIdAsync(user.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                await _userRepository.DeleteAsync(entity);
                return NoContent();
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserCreateUpdateDto userDto)
        {
            var entity = await _userRepository.GetByIdAsync(userDto.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                _mapper.Map(userDto, entity);

                await _userRepository.UpdateAsync(entity);
                return Ok();
            }
        }

        #endregion
    }
}
