using System.ComponentModel.DataAnnotations;

namespace ReservationAPI.DTOs.Write
{
    public class ReservationCreateUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ResourceId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

}
