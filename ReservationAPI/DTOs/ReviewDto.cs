using DAL.Models;

namespace ReservationAPI.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int EscapeRoomId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public DateTime ReviewTime { get; set; } = DateTime.Now;
        public required string EscapeRoomName { get; set; }
        public required string UserName { get; set; }
    }
}
