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
    public class ReviewController : ControllerBase
    {

        private readonly IReviewRepository reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            this.reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        #region Web methods

        [HttpGet]
        public IActionResult GetAllReviews()
        {
            var reviews = reviewRepository.GetAllAsync();
            var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetReviewById(int id)
        {
            var review = reviewRepository.GetByIdAsync(id);
            if (review == null)
                return NotFound();
            return Ok(review);
        }

        [HttpPost]
        public IActionResult AddReview([FromBody] Review review)
        {
            reviewRepository.AddAsync(review);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteReview([FromBody] Review review)
        {
            var entity = reviewRepository.GetByIdAsync(review.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                reviewRepository.DeleteAsync(entity);
                return NoContent();
            }

        }

        [HttpPut]
        public IActionResult UpdateReview([FromBody] Review review)
        {
            var entity = reviewRepository.GetByIdAsync(review.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                entity.Rating = review.Rating;
                entity.Content = review.Content;
                entity.ReviewTime = review.ReviewTime;

                reviewRepository.UpdateAsync(entity);
                return Ok();
            }
        }

        #endregion

    }
}
