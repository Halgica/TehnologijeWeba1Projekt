using System.ComponentModel.DataAnnotations;

namespace ReservationAPI.DTOs.Write
{
    public class TimeSlotCreateUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
        public bool IsReserved { get; set; } = false;
    }
}
