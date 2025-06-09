using AutoMapper;
using DAL.Models;
using DAL.Repos.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.DTOs.Read;
using ReservationAPI.DTOs.Write;

namespace ReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository paymentRepository, IMapper mapper)
        {
            this.paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        #region Web methods

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllPaymentsAsync()
        {
            var payments = await paymentRepository.GetAllAsync();
            var paymentDtos = _mapper.Map<IEnumerable<PaymentDto>>(payments);
            return Ok(paymentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentByIdAsync(int id)
        {
            var payment = await paymentRepository.GetByIdAsync(id);
            if (payment == null)
                return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentAsync([FromBody] PaymentCreateUpdateDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            try
            {
                await paymentRepository.AddAsync(payment);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
            return CreatedAtAction(nameof(GetPaymentByIdAsync).Replace("Async",""), new { id = payment.Id }, paymentDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePaymentAsync([FromBody] PaymentDto payment)
        {
            var entity = await paymentRepository.GetByIdAsync(payment.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                await paymentRepository.DeleteAsync(entity);
                return NoContent();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePaymentAsync([FromBody] PaymentCreateUpdateDto paymentDto)
        {
            var entity = await paymentRepository.GetByIdAsync(paymentDto.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                _mapper.Map(paymentDto,entity);

                await paymentRepository.UpdateAsync(entity);
                return Ok();
            }
        }

        #endregion
    }
}
