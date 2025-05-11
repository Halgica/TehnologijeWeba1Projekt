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
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotRepository timeSlotRepository;
        private readonly IMapper mapper;

        public TimeSlotController(ITimeSlotRepository timeSlotRepository, IMapper mapper)
        {
            this.timeSlotRepository = timeSlotRepository;
            this.mapper = mapper;
        }

        #region Web methods

        [HttpGet]
        public IActionResult GetAllTimeSlots()
        {
            var timeSlotDto = timeSlotRepository.GetAll();
            var TimeSlotsDtos = mapper.Map<IEnumerable<TimeSlotDto>>(timeSlotDto);
            return Ok(TimeSlotsDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetTimeSlotById(int id)
        {
            var timeSlot = timeSlotRepository.GetById(id);
            var timeSlotDto = mapper.Map<TimeSlotDto>(timeSlot);
            if (timeSlot == null)
                return NotFound();
            return Ok(timeSlotDto);
        }

        [HttpPost]
        public IActionResult AddTimeSlot([FromBody] TimeSlotDto timeSlotDto)
        {
            var timeSlot = mapper.Map<TimeSlot>(timeSlotDto);
            timeSlotRepository.Add(timeSlot);
            return Ok(timeSlot);
        }

        [HttpDelete]
        public IActionResult DeleteReservation([FromBody] TimeSlot timeSlot)
        {
            var entity = timeSlotRepository.GetById(timeSlot.Id);
            if (entity == null)
                return NotFound();
            else
            {
                timeSlotRepository.Delete(entity);
                return NoContent();
            }
        }

        [HttpPut]
        public IActionResult UpdateReservation([FromBody] TimeSlotDto timeSlotDto)
        {
            var entity = timeSlotRepository.GetById(timeSlotDto.Id);
            if (entity == null)
                return NotFound();
            else
            {
                mapper.Map(timeSlotDto, entity);

                timeSlotRepository.Update(entity);
                return Ok();
            }
        }

        #endregion
    }
}
