using AutoMapper;
using DAL.Models;
using DAL.Repos.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.DTOs.Read;
using ReservationAPI.DTOs.Write;

namespace ReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionRepository promotionRepository;
        private readonly IMapper mapper;

        public PromotionController(IPromotionRepository promotionRepository, IMapper mapper)
        {
            this.promotionRepository = promotionRepository;
            this.mapper = mapper;
        }

        #region Web methods

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllPromotionsAsync()
        {
            var reservations = await promotionRepository.GetAllAsync();
            var reservationsDtos = mapper.Map<IEnumerable<PromotionDto>>(reservations);
            return Ok(reservationsDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationByIdAsync(int id)
        {
            var reservation = await promotionRepository.GetByIdAsync(id);
            var reservationDto = mapper.Map<PromotionDto>(reservation);
            if (reservation == null)
                return NotFound();
            return Ok(reservationDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddReservationAsync([FromBody] PromotionCreateUpdateDto promotionDto)
        {
            var promotion = mapper.Map<Promotion>(promotionDto);
            await promotionRepository.AddAsync(promotion);
            return CreatedAtAction(nameof(GetReservationByIdAsync).Replace("Async",""), new {id =  promotion.Id}, promotionDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReservationAsync([FromBody] PromotionDto promotionDto)
        {
            var entity = await promotionRepository.GetByIdAsync(promotionDto.Id);
            if (entity == null)
                return NotFound();
            else
            {
                await promotionRepository.DeleteAsync(entity);
                return NoContent();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservationAsync([FromBody] PromotionCreateUpdateDto promotionDto)
        {
            var entity = await promotionRepository.GetByIdAsync(promotionDto.Id);
            if (entity == null)
                return NotFound();
            else
            {
                mapper.Map(promotionDto, entity);

                await promotionRepository.UpdateAsync(entity);
                return Ok();
            }
        }

        #endregion
    }
}
