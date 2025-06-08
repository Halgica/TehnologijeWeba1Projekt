namespace ReservationAPI.DTOs.Auth
{
    public class LoginDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
