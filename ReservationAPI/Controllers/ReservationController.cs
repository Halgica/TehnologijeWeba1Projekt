using AutoMapper;
using DAL.Models;
using DAL.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.DTOs;

namespace ReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IMapper mapper;

        public ReservationController(IReservationRepository reservationRepository, IMapper mapper)
        {
            this.reservationRepository = reservationRepository;
            this.mapper = mapper;
        }

        #region Web methods

        [HttpGet]
        public IActionResult GetAllReservations()
        {
            var reservations = reservationRepository.GetAllAsync();
            var reservationsDtos = mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return Ok(reservationsDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            var reservation = reservationRepository.GetByIdAsync(id);
            var reservationDto = mapper.Map<ReservationDto>(reservation);
            if (reservation == null)
                return NotFound();
            return Ok(reservationDto);
        }

        [HttpPost]
        public IActionResult AddReservation([FromBody] ReservationDto reservationDto)
        {
            var reservation = mapper.Map<Reservation>(reservationDto);
            reservationRepository.AddAsync(reservation);
            return Ok(reservation);
        }

        [HttpDelete]
        public IActionResult DeleteReservation([FromBody] Reservation reservation)
        {
            var entity = reservationRepository.GetByIdAsync(reservation.Id);
            if (entity == null)
                return NotFound();
            else
            {
                reservationRepository.DeleteAsync(entity);
                return NoContent();
            }
        }

        [HttpPut]
        public IActionResult UpdateReservation([FromBody]ReservationDto reservationDto)
        {
            var entity = reservationRepository.GetByIdAsync(reservationDto.Id);
            if (entity == null)
                return NotFound();
            else
            {
                mapper.Map(reservationDto, entity);

                reservationRepository.UpdateAsync(entity);
                return Ok();
            }
        }

        #endregion
    }
}
