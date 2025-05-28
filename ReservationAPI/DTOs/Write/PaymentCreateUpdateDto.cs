using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ReservationAPI.DTOs.Write
{
    public class PaymentCreateUpdateDto
    {
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        public PaymentType Type { get; set; }

        [Required]
        public string? UserName { get; set; }
    }
}
