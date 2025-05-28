using DAL.Models;

namespace ReservationAPI.DTOs.Read
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int EscapeRoomId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string? Content { get; set; }
        public DateTime ReviewTime { get; set; }

        public string UserName { get; set; }
        public string EscapeRoomName { get; set; }
    }

}
