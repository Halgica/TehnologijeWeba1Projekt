using DAL.Models;

namespace ReservationAPI.DTOs.Read
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public PaymentType Type { get; set; }
        public string? UserName { get; set; }
    }
}
