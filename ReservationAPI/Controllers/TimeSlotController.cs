using AutoMapper;
using DAL.Models;
using DAL.Repos.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.DTOs.Read;
using ReservationAPI.DTOs.Write;

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
        public async Task<IActionResult> GetAllTimeSlotsAsync()
        {
            var timeSlotDto = await timeSlotRepository.GetAllAsync();
            var TimeSlotsDtos = mapper.Map<IEnumerable<TimeSlotDto>>(timeSlotDto);
            return Ok(TimeSlotsDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeSlotByIdAsync(int id)
        {
            var timeSlot = await timeSlotRepository.GetByIdAsync(id);
            var timeSlotDto = mapper.Map<TimeSlotDto>(timeSlot);
            if (timeSlot == null)
                return NotFound();
            return Ok(timeSlotDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddTimeSlotAsync([FromBody] TimeSlotCreateUpdateDto timeSlotDto)
        {
            var timeSlot = mapper.Map<TimeSlot>(timeSlotDto);
            await timeSlotRepository.AddAsync(timeSlot);
            return CreatedAtAction(nameof(GetTimeSlotByIdAsync).Replace("Async", ""), new { id = timeSlot.Id }, timeSlotDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReservationAsync([FromBody] TimeSlotDto timeSlotDto)
        {
            var entity = await timeSlotRepository.GetByIdAsync(timeSlotDto.Id);
            if (entity == null)
                return NotFound();
            else
            {
                await timeSlotRepository.DeleteAsync(entity);
                return NoContent();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservationAsync([FromBody] TimeSlotCreateUpdateDto timeSlotDto)
        {
            var entity = await timeSlotRepository.GetByIdAsync(timeSlotDto.Id);
            if (entity == null)
                return NotFound();
            else
            {
                mapper.Map(timeSlotDto, entity);

                await timeSlotRepository.UpdateAsync(entity);
                return Ok();
            }
        }

        #endregion
    }
}
