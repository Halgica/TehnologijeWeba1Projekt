namespace ReservationAPI.DTOs.Read
{
    public class PromotionDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public required string Code { get; set; }
        public float DiscountAmount { get; set; }
        public DateTime PromotionEndTime { get; set; }
        public int UsageCount { get; set; }
    }
}
