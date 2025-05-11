using DAL.Models;

namespace ReservationAPI.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public PaymentType Type { get; set; }
        public string? UserName { get; set; }
    }
}
