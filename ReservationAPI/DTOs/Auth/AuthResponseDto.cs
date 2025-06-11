using DAL.Models;

namespace ReservationAPI.DTOs.Auth
{
    public class AuthResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public virtual AuthRole Role { get; set; } = null!; // Dopusti Null
    }
}
