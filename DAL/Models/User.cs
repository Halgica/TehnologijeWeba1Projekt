namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; } = 0;
        public virtual IList<Reservation>? Reservations { get; set; }
    }
}
