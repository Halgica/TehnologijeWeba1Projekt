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

        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var payments = paymentRepository.GetAll();
            var paymentDtos = _mapper.Map<IEnumerable<PaymentDto>>(payments);
            return Ok(paymentDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentById(int id)
        {
            var payment = paymentRepository.GetById(id);
            if (payment == null)
                return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public IActionResult AddPayment([FromBody] Payment payment)
        {
            paymentRepository.Add(payment);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeletePayment([FromBody] Payment payment)
        {
            var entity = paymentRepository.GetById(payment.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                paymentRepository.Delete(entity);
                return NoContent();
            }
        }

        [HttpPut]
        public IActionResult UpdatePayment([FromBody] Payment payment)
        {
            var entity = paymentRepository.GetById(payment.Id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                entity.UserId = payment.UserId;
                entity.Type = payment.Type;
                entity.User = payment.User;

                paymentRepository.Update(entity);
                return Ok();
            }
        }

        #endregion
    }
}
