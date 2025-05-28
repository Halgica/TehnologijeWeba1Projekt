using DAL.Models;

namespace ReservationAPI.DTOs.Read
{
    public class RoleDto
    {
        public int Id { get; set; }
        public RoleName Name { get; set; }
    }
}
