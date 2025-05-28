using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ReservationAPI.DTOs.Write
{
    public class RoleCreateUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public RoleName Name { get; set; }
    }
}
