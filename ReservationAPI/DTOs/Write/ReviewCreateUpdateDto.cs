using System.ComponentModel.DataAnnotations;

namespace ReservationAPI.DTOs.Write
{
    public class ReviewCreateUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public int EscapeRoomId { get; set; }

        [Required]
        public int UserId { get; set; }

        public int Rating { get; set; }
        public string? Content { get; set; }
        public DateTime ReviewTime { get; set; } = DateTime.Now;
    }

}
