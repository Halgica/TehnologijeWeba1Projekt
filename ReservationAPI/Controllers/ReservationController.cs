using AutoMapper;
using DAL.DB;
using DAL.Models;
using DAL.Repos.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.DTOs.Read;
using ReservationAPI.DTOs.Write;
using System.Security.Claims;

namespace ReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IMapper _mapper;
        private readonly TW1DbContext _context;

        public ReservationController(IReservationRepository reservationRepository, IMapper mapper, TW1DbContext context)
        {
            this.reservationRepository = reservationRepository;
            _mapper = mapper;
            _context = context;
        }

        #region Web methods

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            var reservations = await reservationRepository.GetAllAsync();
            var reservationsDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return Ok(reservationsDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationByIdAsync(int id)
        {
            var reservation = await reservationRepository.GetByIdAsync(id);
            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            if (reservation == null)
                return NotFound();
            return Ok(reservationDto);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddReservationAsync([FromBody] ReservationCreateUpdateDto reservationDto)
        //{
        //    var reservation = mapper.Map<Reservation>(reservationDto);
        //    await reservationRepository.AddAsync(reservation);
        //    return CreatedAtAction(nameof(GetReservationByIdAsync).Replace("Async",""), new {id = reservation.Id}, reservationDto);
        //}
        [HttpPost]
        public async Task<IActionResult> AddReservationAsync([FromBody] ReservationCreateUpdateDto dto)
        {
            var reservation = _mapper.Map<Reservation>(dto);

            reservation.User = await _context.AuthUsers.FindAsync(dto.UserId);
            reservation.Resource = await _context.Resources.FindAsync(dto.ResourceId);

            if (reservation.User == null || reservation.Resource == null)
            {
                return BadRequest("Invalid UserId or ResourceId.");
            }

            _context.Reservations.Add(reservation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }

            var result = _mapper.Map<ReservationDto>(reservation);
            return CreatedAtAction(nameof(GetReservationByIdAsync).Replace("Async", ""), new { id = reservation.Id }, result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReservationAsync([FromBody] ReservationDto reservationDto)
        {
            var entity = await reservationRepository.GetByIdAsync(reservationDto.Id);
            if (entity == null)
                return NotFound();
            else
            {
                await reservationRepository.DeleteAsync(entity);
                return NoContent();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservationAsync([FromBody]ReservationCreateUpdateDto reservationDto)
        {
            var entity = await reservationRepository.GetByIdAsync(reservationDto.Id);
            if (entity == null)
                return NotFound();
            else
            {
                _mapper.Map(reservationDto, entity);

                await reservationRepository.UpdateAsync(entity);
                return Ok(reservationDto);
            }
        }

        [HttpGet("GetUserReservations")]
        public async Task<IActionResult> GetUserReservationsAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid or missing user ID.");
            }

            var userReservations = await _context.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Resource)
                .ToListAsync();

            var reservationDtos = _mapper.Map<List<ReservationDto>>(userReservations);

            return Ok(reservationDtos);
        }

        #endregion
    }
}
