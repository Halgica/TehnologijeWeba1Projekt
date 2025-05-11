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
            var reservations = promotionRepository.GetAll();
            var reservationsDtos = mapper.Map<IEnumerable<PromotionDto>>(reservations);
            return Ok(reservationsDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            var reservation = promotionRepository.GetById(id);
            var reservationDto = mapper.Map<PromotionDto>(reservation);
            if (reservation == null)
                return NotFound();
            return Ok(reservationDto);
        }

        [HttpPost]
        public IActionResult AddReservation([FromBody] PromotionDto promotionDto)
        {
            var promotion = mapper.Map<Promotion>(promotionDto);
            promotionRepository.Add(promotion);
            return Ok(promotion);
        }

        [HttpDelete]
        public IActionResult DeleteReservation([FromBody] Promotion promotion)
        {
            var entity = promotionRepository.GetById(promotion.Id);
            if (entity == null)
                return NotFound();
            else
            {
                promotionRepository.Delete(entity);
                return NoContent();
            }
        }

        [HttpPut]
        public IActionResult UpdateReservation([FromBody] PromotionDto promotionDto)
        {
            var entity = promotionRepository.GetById(promotionDto.Id);
            if (entity == null)
                return NotFound();
            else
            {
                mapper.Map(promotionDto, entity);

                promotionRepository.Update(entity);
                return Ok();
            }
        }

        #endregion
    }
}
