using System.ComponentModel.DataAnnotations;

namespace ReservationAPI.DTOs.Write
{
    public class UserCreateUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
