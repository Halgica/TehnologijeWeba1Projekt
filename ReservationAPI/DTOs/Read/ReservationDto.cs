namespace ReservationAPI.DTOs.Read
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ResourceId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string UserName { get; set; }
        public string ResourceName { get; set; }
    }

}
