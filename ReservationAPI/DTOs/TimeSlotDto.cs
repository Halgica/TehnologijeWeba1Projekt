namespace ReservationAPI.DTOs
{
    public class TimeSlotDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsReserved { get; set; } = false;
    }
}
