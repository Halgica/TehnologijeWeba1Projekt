using System.ComponentModel.DataAnnotations;

namespace ReservationAPI.DTOs.Write
{
    public class PromotionCreateUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public required string Code { get; set; }
        public float DiscountAmount { get; set; }
        public DateTime PromotionEndTime { get; set; }
        public int UsageCount { get; set; }
    }
}
