using DAL.Models;

namespace ReservationAPI.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }
        public required RoleName Name { get; set; }
    }
}
