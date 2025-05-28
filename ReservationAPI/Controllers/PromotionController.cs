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

        [HttpGet]
        public IActionResult GetAllPromotions()
        {
            var reservations = promotionRepository.GetAllAsync();
            var reservationsDtos = mapper.Map<IEnumerable<PromotionDto>>(reservations);
            return Ok(reservationsDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            var reservation = promotionRepository.GetByIdAsync(id);
            var reservationDto = mapper.Map<PromotionDto>(reservation);
            if (reservation == null)
                return NotFound();
            return Ok(reservationDto);
        }

        [HttpPost]
        public IActionResult AddReservation([FromBody] PromotionDto promotionDto)
        {
            var promotion = mapper.Map<Promotion>(promotionDto);
            promotionRepository.AddAsync(promotion);
            return Ok(promotion);
        }

        [HttpDelete]
        public IActionResult DeleteReservation([FromBody] Promotion promotion)
        {
            var entity = promotionRepository.GetByIdAsync(promotion.Id);
            if (entity == null)
                return NotFound();
            else
            {
                promotionRepository.DeleteAsync(entity);
                return NoContent();
            }
        }

        [HttpPut]
        public IActionResult UpdateReservation([FromBody] PromotionDto promotionDto)
        {
            var entity = promotionRepository.GetByIdAsync(promotionDto.Id);
            if (entity == null)
                return NotFound();
            else
            {
                mapper.Map(promotionDto, entity);

                promotionRepository.UpdateAsync(entity);
                return Ok();
            }
        }

        #endregion
    }
}
