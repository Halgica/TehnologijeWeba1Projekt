using AutoMapper;
using DAL.Models;
using DAL.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.DTOs;

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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAll();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);


        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet("search")]
        public IActionResult SearchUsers([FromQuery] string? name, [FromQuery] string? email)
        {
            var users = _userRepository.Find(u =>
                (string.IsNullOrEmpty(name) || u.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(email) || u.Email.Contains(email, StringComparison.OrdinalIgnoreCase))
            );

            if (!users.Any())
                return NotFound("No users found matching the criteria.");

            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserCreateUpdateDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _userRepository.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userDto);
        }

        [HttpDelete]
        public IActionResult DeleteUser([FromBody] UserDto user)
        {
            var entity = _userRepository.GetById(user.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                _userRepository.Delete(entity);
                return NoContent();
            }

        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserCreateUpdateDto userDto)
        {
            var entity = _userRepository.GetById(userDto.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                _mapper.Map(userDto, entity);

                _userRepository.Update(entity);
                return Ok();
            }
        }

        #endregion
    }
}
