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
    public class ReviewController : ControllerBase
    {

        private readonly IReviewRepository reviewRepository;
        private readonly IMapper _mapper;
        private readonly TW1DbContext _context;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, TW1DbContext context)
        {
            this.reviewRepository = reviewRepository;
            _mapper = mapper;
            _context = context;
        }

        #region Web methods

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllReviewsAsync()
        {
            var reviews = await reviewRepository.GetAllAsync();
            var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewByIdAsync(int id)
        {
            var review = await reviewRepository.GetByIdAsync(id);
            if (review == null)
                return NotFound();
            return Ok(review);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddReviewAsync([FromBody] ReviewCreateUpdateDto reviewDto)
        //{
        //    var review = _mapper.Map<Review>(reviewDto);
        //    review.User = await reviewRepository.Users.FindAsync(reviewDto.UserId);
        //    review.EscapeRoom = await reviewRepository.Resources.FindAsync(reviewDto.EscapeRoomId);
        //    await reviewRepository.AddAsync(review);
        //    return CreatedAtAction(nameof(GetReviewByIdAsync).Replace("Async", ""), new { id = review.Id }, reviewDto);
        //}
        [HttpPost]
        public async Task<IActionResult> AddReviewAsync([FromBody] ReviewCreateUpdateDto dto)
        {
            var review = _mapper.Map<Review>(dto);

            review.User = await _context.AuthUsers.FindAsync(dto.UserId);
            review.EscapeRoom = await _context.Resources.FindAsync(dto.EscapeRoomId);

            if (review.User == null || review.EscapeRoom == null)
            {
                return BadRequest("Invalid UserId or EscapeRoomId.");
            }

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            var reviewDto = _mapper.Map<ReviewDto>(review);
            return CreatedAtAction(nameof(GetReviewByIdAsync).Replace("Async", ""), new { id = review.Id }, reviewDto);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteReviewAsync([FromBody] ReviewDto review)
        {
            var entity = await reviewRepository.GetByIdAsync(review.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                await reviewRepository.DeleteAsync(entity);
                return NoContent();
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateReviewAsync([FromBody] ReviewCreateUpdateDto reviewDto)
        {
            var entity = await reviewRepository.GetByIdAsync(reviewDto.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                _mapper.Map(reviewDto, entity);

                await reviewRepository.UpdateAsync(entity);
                return Ok();
            }
        }

        [HttpGet("GetUserReview")]
        public async Task<IActionResult> GetUserReviewsAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid or missing user ID.");
            }

            var userReviews = await _context.Reviews
                .Where(r => r.User.Id == userId)  
                .Include(r => r.User)
                .Include(r => r.EscapeRoom)
                .ToListAsync();

            var reviewDtos = _mapper.Map<List<ReviewDto>>(userReviews);

            return Ok(reviewDtos);
        }

        #endregion

    }
}
