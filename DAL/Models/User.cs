namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public int RoleId { get; set; } = 1;
        public virtual IList<Reservation>? Reservations { get; set; }
    }
}
