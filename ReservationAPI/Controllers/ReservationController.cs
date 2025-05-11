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
            var reservations = reservationRepository.GetAll();
            var reservationsDtos = mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return Ok(reservationsDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            var reservation = reservationRepository.GetById(id);
            var reservationDto = mapper.Map<ReservationDto>(reservation);
            if (reservation == null)
                return NotFound();
            return Ok(reservationDto);
        }

        [HttpPost]
        public IActionResult AddReservation([FromBody] ReservationDto reservationDto)
        {
            var reservation = mapper.Map<Reservation>(reservationDto);
            reservationRepository.Add(reservation);
            return Ok(reservation);
        }

        [HttpDelete]
        public IActionResult DeleteReservation([FromBody] Reservation reservation)
        {
            var entity = reservationRepository.GetById(reservation.Id);
            if (entity == null)
                return NotFound();
            else
            {
                reservationRepository.Delete(entity);
                return NoContent();
            }
        }

        [HttpPut]
        public IActionResult UpdateReservation([FromBody]ReservationDto reservationDto)
        {
            var entity = reservationRepository.GetById(reservationDto.Id);
            if (entity == null)
                return NotFound();
            else
            {
                mapper.Map(reservationDto, entity);

                reservationRepository.Update(entity);
                return Ok();
            }
        }

        #endregion
    }
}
